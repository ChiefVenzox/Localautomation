# LocalFramework

LocalFramework is a local automation application for businesses, designed to scan and validate QR codes with a strong validation system. It is built using .NET MAUI, leveraging `ZXing.Net.MAUI` for QR code scanning and `Venzox.Validation` for robust data validation. Scan records are stored locally using SQLite.

## Features

*   **QR Code Scanning:** Utilizes the device's camera to scan QR codes.
*   **Strong Validation:** Integrates the `Venzox.Validation` framework to enforce business rules on scanned data.
*   **Local Data Storage:** Stores scan records and validation results in a local SQLite database.
*   **User Feedback:** Provides immediate visual feedback on scan results and validation status.

## Technologies Used

*   **.NET MAUI:** For cross-platform desktop application development.
*   **ZXing.Net.MAUI:** For efficient QR code scanning.
*   **Venzox.Validation:** A custom fluent validation framework.
*   **SQLite.NET-PCL:** For local database operations.

## How to Set Up and Run

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/ChiefVenzox/Localautomation.git
    cd LocalFramework
    ```

2.  **Restore NuGet packages:**
    ```bash
    dotnet restore
    ```

3.  **Build the solution:**
    ```bash
    dotnet build
    ```

4.  **Run the application:**
    Choose your desired target platform (e.g., Windows, Android):
    ```bash
    dotnet run -f net8.0-windows10.0.19041.0 # For Windows
    dotnet run -f net8.0-android          # For Android
    # Other targets: net8.0-ios, net8.0-maccatalyst
    ```

    *Note: For Android, ensure you have an emulator running or a physical device connected and configured for development.*

## Usage

1.  Launch the application.
2.  Grant camera permissions when prompted.
3.  Point your device's camera at a QR code.
4.  The application will scan the code, display its content, and indicate whether it passed or failed validation.
5.  Validation rules currently require the scanned QR code to contain the text "Venzox".

## Project Structure

*   `LocalFramework/`: The main .NET MAUI application project.
    *   `Models/QrCodeRecord.cs`: Defines the structure for storing scanned QR code data in SQLite.
    *   `Services/DatabaseService.cs`: Handles all interactions with the SQLite database.
    *   `Constants.cs`: Defines constants for database filename and flags.
    *   `MainPage.xaml`: The XAML markup for the main application UI.
    *   `MainPage.xaml.cs`: The code-behind for the main UI, handling QR scanning, validation, and data saving.
    *   `MauiProgram.cs`: Application entry point and configuration, including `ZXing.Net.MAUI` and dependency injection for `DatabaseService`.
*   `../visualstframework/Venzox.Validation/`: (Referenced project) The custom validation framework used.

---
**Note:** This is a prototype. The validation logic in `QrCodeValidator` (specifically `BeAValidQrCode`) is a placeholder and should be replaced with actual business logic. The data stored in SQLite (`QrCodeRecord`) can be extended based on specific business needs.