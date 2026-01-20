# Pinyin Ruby Tool

A client-side Blazor WebAssembly application that converts Chinese text into Pinyin with HTML `<ruby>` annotations.

## 🚀 Current Status: Completed

The application is fully functional with the following features:

### ✨ Features
*   **Chinese to Pinyin Conversion**: Uses `PinyinM.Net` for high-performance conversion (Offline/Client-side).
*   **Ruby Annotation Display**: Renders Pinyin above characters using standard `<ruby>` tags.
*   **Interactive Selection**:
    *   **Click**: Select individual characters (turns Red).
    *   **Keyboard Navigation**:
        *   `Left` / `<` / `,`: Move selection Previous.
        *   `Right` / `>` / `.`: Move selection Next.
        *   `Up` / `Down`: Jump to Previous/Next block of Chinese characters (skipping punctuation).
*   **Static Deployment**: No backend server required. Runs entirely in the browser.

### 🛠 Tech Stack
*   **.NET 8.0**
*   **Blazor WebAssembly** (Standalone)
*   **PinyinM.Net** (v2.0.0)

## 📦 Deployment & Usage

### 1. Build
To generate the static files:
```bash
dotnet publish -c Release -o dist
```
*Note: This command automatically generates the `dist` folder.*

### 2. Run Locally
Since modern browsers block WebAssembly on `file://` protocol, you must serve the `dist/wwwroot` folder via HTTP.

**Using Python:**
```bash
cd dist/wwwroot
python -m http.server 8000
```
Then open `http://localhost:8000`.

**Using VS Code:**
*   Right-click `dist/wwwroot/index.html` -> "Open with Live Server".

## 📂 Project Structure
*   `Pages/Home.razor`: Main UI and logic.
*   `Services/PinyinService.cs`: Wraps the Pinyin conversion logic.
*   `dist/wwwroot`: The final build output (Ready for GitHub Pages, Netlify, etc.).

## ✅ Recent Updates
*   Removed unused `Counter` and `Weather` pages.
*   Removed raw HTML output pane.
*   Implemented block-skipping navigation for Up/Down keys.
*   Fixed selection issues with punctuation.
