# ImageToWebpConverter

A native Windows 11 desktop application built with **C#** and **WinUI 3** for high-performance batch image conversion to the modern **WebP** format.

[![Windows 11](https://img.shields.io/badge/Platform-Windows%2011%20%2F%2010-blue)](https://microsoft.com)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE.txt)
[![Languages](https://img.shields.io/badge/Languages-7%20Supported-orange)](#supported-languages)

---

## Readme Translations

* 🇮🇹 [Italiano / Italian](#-descrizione-in-italiano)
* 🇬🇧 English (Current / Predefinita)

---

## Key Features

* **Fluent Design & Mica:** Modern UI seamlessly integrated with Windows 11 backdrop effects.
* **Multi-threaded Batch Conversion:** Multi-core parallel processing using `Parallel.ForEach` for maximum throughput.
* **Drag & Drop Support:** Drop files or entire folders directly into the app.
* **EXIF Orientation Preservation:** Retains original orientation data during conversion.
* **Compression Quality Control:** Real-time quality slider adjustment (1% to 100%).
* **Supported Input Formats:** JPG, JPEG, PNG, BMP, WEBP, TIF, TIFF, GIF, AVIF.
* **Automatic File Collision Handling:** Smart progressive renaming to prevent accidental overwrites.
* **Native Localization:** Automatically adapts to OS language settings.

---

## Supported Languages

The application UI automatically adjusts based on Windows system language settings:

* 🇬🇧 **English** (`en-US`)
* 🇮🇹 **Italian** (`it-IT`)
* 🇩🇪 **German** (`de-DE`)
* 🇪🇸 **Spanish** (`es-ES`)
* 🇨🇳 **Chinese Simplified** (`zh-CN`)
* 🇷🇺 **Russian** (`ru-RU`)
* 🇺🇦 **Ukrainian** (`uk-UA`)

---

## Download & Run

No setup or extraction required.

1. Go to the [Releases](../../releases) section of the repository.
2. Download the latest-version executable (e.g., `ImageToWebpConverter-v1.0.2-win-x64.exe`).
3. Double-click to run directly.

---

## Tech Stack

* **Language:** C# / .NET 8
* **UI Framework:** WinUI 3 (Windows App SDK)
* **Image Processing Engine:** Magick.NET (ImageMagick Q8-x64)

---

## License

Distributed under the [MIT License](LICENSE.txt).

---
---

## 🇮🇹 Descrizione in Italiano

**ImageToWebpConverter** è un'applicazione desktop nativa per Windows 11 sviluppata in **C#** e **WinUI 3** per la conversione batch ad alte prestazioni di immagini nel formato **WebP**.

### Caratteristiche Principali

* **Interfaccia Fluent / Material 3:** Design moderno integrato con l'effetto *Mica* di Windows 11.
* **Conversione Batch Multi-thread:** Elaborazione parallela nativa basata sui core della CPU per massimizzare la velocità.
* **Drag & Drop:** Trascina direttamente file e cartelle nella finestra dell'applicazione.
* **Mantenimento Dati EXIF:** Preservazione dell'orientamento dell'immagine durante la conversione.
* **Controllo Qualità:** Regolazione dinamica della compressione WebP tramite slider (da 1% a 100%).
* **Formati d'ingresso supportati:** JPG, JPEG, PNG, BMP, WEBP, TIF, TIFF, GIF, AVIF.
* **Gestione Nomi Duplicati:** Riconoscimento automatico e rinomina progressiva senza sovrascrittura.
* **Localizzazione Automatica:** Supporto nativo per 7 lingue con rilevamento automatico della lingua di sistema.

### Stack Tecnologico

* **Linguaggio:** C# / .NET 8
* **UI Framework:** WinUI 3 (Windows App SDK)
* **Image Processing Engine:** Magick.NET (ImageMagick Q8-x64)

### Download e Avvio

1. Vai alla sezione [Releases](../../releases) del repository.
2. Scarica l'eseguibile dell'ultima versione (es. `ImageToWebpConverter-v1.0.2-win-x64.exe`).
3. Avvialo direttamente con doppio-clic.

### Licenza

Questo progetto è rilasciato sotto licenza [MIT License](LICENSE.txt).