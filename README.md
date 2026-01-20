# Pinyin Ruby Tool

A client-side Blazor WebAssembly application that converts Chinese text into Pinyin/Zhuyin with HTML `<ruby>` annotations.

## 🚀 Current Status: Completed

The application is fully functional with the following features:

### ✨ Features
*   **Chinese to Pinyin Conversion**: Uses `PinyinM.Net` for high-performance conversion (Offline/Client-side).
*   **Chinese to Zhuyin Conversion**: Custom Pinyin-to-Zhuyin mapping for Taiwanese Bopomofo system.
*   **Ruby Annotation Display**: Renders Pinyin or Zhuyin above characters using standard `<ruby>` tags.
*   **Mode Selector**: Toggle between Pinyin and Zhuyin (注音) display modes.
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
*   `Pages/Home.razor`: Main UI with Pinyin/Zhuyin mode toggle.
*   `Services/PinyinService.cs`: Handles Pinyin conversion and Pinyin-to-Zhuyin mapping.
*   `Services/RubyToken.cs`: Data model supporting both Pinyin and Zhuyin.
*   `dist/wwwroot`: The final build output (Ready for GitHub Pages, Netlify, etc.).

## ✅ Recent Updates
*   Added Zhuyin (注音) ruby mode with Pinyin-to-Zhuyin conversion mapping.
*   Added mode selector button in the result header to switch between Pinyin and Zhuyin.
*   Updated RubyToken model to store both Pinyin and Zhuyin values.
