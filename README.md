# ImageToWebpConverter

Un'applicazione desktop nativa per Windows 11 sviluppata in **C#** e **WinUI 3** per la conversione batch ad alte prestazioni di immagini nel formato modernizzato **WebP**.

![Windows 11](https://img.shields.io/badge/Platform-Windows%2011%20%2F%2010-blue)
![.NET 8](https://img.shields.io/badge/.NET-8.0-purple)
![License](https://img.shields.io/badge/License-MIT-green)

---

## Caratteristiche Principali

* **Interfaccia Fluent / Material 3:** Design moderno integrato con l'effetto *Mica* di Windows 11.
* **Conversione Batch Multi-thread:** Elaborazione parallela nativa tramite `Parallel.ForEach` basata sui core della CPU per massimizzare la velocità.
* **Drag & Drop:** Trascina direttamente file e cartelle nella finestra dell'applicazione.
* **Controllo Qualità:** Regolazione dinamica della compressione WebP tramite slider (da 1% a 100%).
* **Formati d'ingresso supportati:** JPG, JPEG, PNG, BMP, WEBP, TIF, TIFF, GIF, AVIF.
* **Gestione Nomi Duplicati:** Riconoscimento automatico e rinomina progressiva senza sovrascrittura accidentale.

---

## Download e Installazione

Non richiede alcuna installazione.

1. Vai nella sezione [Releases](../../releases) del repository.
2. Scarica l'archivio ZIP dell'ultima versione (es. `ImageToWebpConverter-v1.0.0-win-x64.zip`).
3. Estrai il contenuto in una cartella a tua scelta.
4. Esegui il file `ImageToWebpConverter.exe`.

---

## Stack Tecnologico

* **Linguaggio:** C# / .NET 8
* **UI Framework:** WinUI 3 (Windows App SDK)
* **Image Processing Engine:** Magick.NET (ImageMagick Q8-x64)

---

## Licenza

Questo progetto è rilasciato sotto licenza [MIT](LICENSE).
