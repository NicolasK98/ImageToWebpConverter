using ImageMagick;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT;
using WinUIEx;

namespace ImageToWebpConverter
{
    public sealed partial class MainWindow : Window
    {
        private List<string> selectedFiles = new();
        private bool isWindowInitialized = false;
        private readonly ResourceLoader resourceLoader;

        private static readonly HashSet<string> SupportedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".webp", ".tif", ".tiff", ".gif", ".avif"
        };

        public MainWindow()
        {
            this.InitializeComponent();

            resourceLoader = new ResourceLoader();

            ApplyLocalizedStrings();

            this.SystemBackdrop = new MicaBackdrop();
            this.ExtendsContentIntoTitleBar = true;
            this.Activated += MainWindow_Activated;
        }

        private void ApplyLocalizedStrings()
        {
            this.Title = resourceLoader.GetString("AppTitle");
            TxtAppTitle.Text = resourceLoader.GetString("AppTitle");
            TxtDragHeader.Text = resourceLoader.GetString("DragText");
            TxtDragSubHeader.Text = resourceLoader.GetString("DragSubText");
            BtnSelect.Content = resourceLoader.GetString("SelectBtn");
            TxtStatus.Text = resourceLoader.GetString("StatusNoFiles");
            BtnConvert.Content = resourceLoader.GetString("ConvertBtn");
            UpdateQualityText((int)SliderQuality.Value);
        }

        private void UpdateQualityText(int quality)
        {
            string format = resourceLoader.GetString("QualityLabel");
            TxtQuality.Text = string.Format(format, quality);
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (!isWindowInitialized)
            {
                isWindowInitialized = true;
                this.SetTitleBar(AppTitleBar);
                SetWindowSize(460, 480);
                SetAppIcon();
            }
        }

        private void SetAppIcon()
        {
            try
            {
                string iconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "appicon.ico");
                if (!File.Exists(iconPath))
                {
                    iconPath = Path.Combine(AppContext.BaseDirectory, "appicon.ico");
                }

                if (File.Exists(iconPath))
                {
                    this.AppWindow.SetIcon(iconPath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Errore caricamento icona: {ex.Message}");
            }
        }

        #region Drag and Drop Logic
        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
            e.DragUIOverride.Caption = resourceLoader.GetString("DragCaption");
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                var files = items.OfType<StorageFile>()
                                 .Where(f => SupportedExtensions.Contains(Path.GetExtension(f.Path)))
                                 .Select(f => f.Path)
                                 .ToList();

                if (files.Count > 0)
                {
                    selectedFiles = files;
                    string format = resourceLoader.GetString("StatusReady");
                    TxtStatus.Text = string.Format(format, selectedFiles.Count);
                    BtnConvert.IsEnabled = true;
                    ProgBar.Value = 0;
                }
            }
        }
        #endregion

        #region Conversione e Interazione
        private async void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hwnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            foreach (var ext in SupportedExtensions)
            {
                openPicker.FileTypeFilter.Add(ext);
            }

            var files = await openPicker.PickMultipleFilesAsync();

            if (files != null && files.Count > 0)
            {
                selectedFiles = files.Select(f => f.Path).ToList();
                string format = resourceLoader.GetString("StatusSelected");
                TxtStatus.Text = string.Format(format, selectedFiles.Count);
                BtnConvert.IsEnabled = true;
                ProgBar.Value = 0;
            }
        }

        private void SliderQuality_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (TxtQuality != null && resourceLoader != null)
            {
                UpdateQualityText((int)e.NewValue);
            }
        }

        private async void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFiles.Count == 0) return;

            var folderPicker = this.CreateFolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            folderPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            var storageFolder = await folderPicker.PickSingleFolderAsync();
            if (storageFolder == null) return;

            BtnConvert.IsEnabled = false;
            BtnSelect.IsEnabled = false;
            int quality = (int)SliderQuality.Value;

            string outputFolder = storageFolder.Path;
            int total = selectedFiles.Count;
            int completedCount = 0;

            var uiDispatcher = Windows.System.DispatcherQueue.GetForCurrentThread();

            await Task.Run(() =>
            {
                Parallel.ForEach(selectedFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, filePath =>
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string destinationPath = GetUniqueFilePath(outputFolder, fileName, ".webp");

                        using (var image = new MagickImage(filePath))
                        {
                            image.AutoOrient();
                            image.Format = MagickFormat.WebP;
                            image.Quality = (uint)quality;
                            image.Write(destinationPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Errore durante la conversione di {filePath}: {ex.Message}");
                    }
                    finally
                    {
                        int currentCompleted = Interlocked.Increment(ref completedCount);
                        int currentProgress = (int)(((double)currentCompleted / total) * 100);

                        uiDispatcher?.TryEnqueue(() =>
                        {
                            ProgBar.Value = currentProgress;
                        });
                    }
                });
            });

            TxtStatus.Text = resourceLoader.GetString("StatusCompleted");
            BtnSelect.IsEnabled = true;
            BtnConvert.IsEnabled = false;
            selectedFiles.Clear();
        }

        private static string GetUniqueFilePath(string folder, string fileName, string extension)
        {
            string destinationPath = Path.Combine(folder, $"{fileName}{extension}");
            int count = 1;

            while (File.Exists(destinationPath))
            {
                destinationPath = Path.Combine(folder, $"{fileName}_{count}{extension}");
                count++;
            }

            return destinationPath;
        }

        private void SetWindowSize(int width, int height)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                double scaleFactor = DisplayInformationScale(hwnd);
                int scaledWidth = (int)(width * scaleFactor);
                int scaledHeight = (int)(height * scaleFactor);

                appWindow.Resize(new Windows.Graphics.SizeInt32(scaledWidth, scaledHeight));

                if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                {
                    presenter.IsResizable = false;
                    presenter.IsMaximizable = false;
                }
            }
        }

        private double DisplayInformationScale(IntPtr hwnd)
        {
            uint dpi = GetDpiForWindow(hwnd);
            return dpi / 96.0;
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hwnd);
        #endregion
    }
}