<div align="center">

<a href="https://github.com/ignYoqzii/GuessMate/graphs/contributors">![Contributors](https://img.shields.io/github/contributors/ignYoqzii/GuessMate?style=flat)</a>
<a href="https://github.com/ignYoqzii/GuessMate/network/members">![Forks](https://img.shields.io/github/forks/ignYoqzii/GuessMate?style=flat)</a>
<a href="https://github.com/ignYoqzii/GuessMate/stargazers">![Stargazers](https://img.shields.io/github/stars/ignYoqzii/GuessMate?style=flat)</a>
<a href="https://github.com/ignYoqzii/GuessMate/issues">![Issues](https://img.shields.io/github/issues/ignYoqzii/GuessMate?style=flat)</a>
<a href="https://github.com/ignYoqzii/GuessMate/blob/main/LICENSE">![License](https://img.shields.io/github/license/ignYoqzii/GuessMate?style=flat)</a>

</div>

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/ignYoqzii/GuessMate">
    <img src="/GuessMate/Assets/GuessMateLogoFull.png" alt="GuessMate Logo" />
  </a>

  <h3 align="center">GuessMate</h3>

  <p align="center">
    GuessMate is a Wordle companion desktop application for Windows. Enter your Wordle guesses and GuessMate will recreate the board visually, filter the ~14k+ valid words to help find solutions, and show hint resources via an embedded WebView.
    Built with Lepoco WPF UI for a modern Fluent-style interface.
  <br />
  <br />
  <a href="https://github.com/ignYoqzii/GuessMate">
    <img src="/GuessMate/Assets/Screenshot1.png" alt="GuessMate Screenshot" />
  </a>
  <br />
  </p>
</p>

## Features

- **Board Recreation**: Recreate the Wordle board by inputting your guesses and marking letter states (green/yellow/gray).
- **Smart Filtering**: Filters the official/extended wordlist (≈14k+) using greens/yellows/blacks to show remaining candidate answers and handles duplicate-letter cases.
- **Wordle Solution Fetching**: Optionally fetch the official puzzle solution (when available and permitted).
- **Embedded Hints**: Built-in WebView (Edge WebView2) to display NYT Wordle or other hint resources inside the app.
- **Themeable UI**: Light / Dark theme support via Lepoco WPF UI.
- **Keyboard Shortcuts & Quick Entry**: Fast keyboard-driven guess entry and navigation.
- **Export / Import Sessions**: Save and load session files containing board state and filters.
- **Configurable Wordlists**: Use bundled lists or point the app to a custom wordlist.

## Requirements

- Windows 10 / Windows 11 (desktop)

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/)
- [Microsoft Edge WebView2 runtime](https://developer.microsoft.com/en-us/microsoft-edge/webview2/)
- Visual Studio 2022+ (recommended) or the `dotnet` CLI

## Configuration

GuessMate reads configuration from `%USERPROFILE%\Documents\GuessMate\Config.txt`. Example `Config.txt` (KEY = VALUE):

```
Theme = Dark
```

## Usage

1. Launch GuessMate.
2. Enter guesses row-by-row. For each guess, set letter states by selecting the color from a dropdown.
3. The guesses appears directly on a recreation of the Wordle's Board. You can then filter the word's list based on previous constraints.
5. Open the embedded WebView for NYT Wordle hints or simply reveal the solution if you're stuck.
6. Save or export session files to preserve board and filters.

## Word Filtering Behavior

- **Green (correct position)**: letter fixed at position.
- **Yellow (present, wrong position)**: letter must appear at least once but not in that position.
- **Gray (absent)**: letter excluded (with correct handling for duplicates).

The filtering algorithm includes special cases for duplicate letters and positional counts — add unit tests when modifying logic.

  <a href="https://github.com/ignYoqzii/GuessMate">
    <img src="/GuessMate/Assets/Screenshot2.png" alt="GuessMate Screenshot" />
  </a>

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [WPF-UI (lepoco)](https://github.com/lepoco/wpfui) — Fluent-style WPF components used for the UI.
- [Microsoft Edge WebView2](https://developer.microsoft.com/en-us/microsoft-edge/webview2/) — Embedded web content.
- New York Times — Wordle web interface (used in-app via WebView for hints).
- Thanks to the Wordle community for the full 14k+ word list.
