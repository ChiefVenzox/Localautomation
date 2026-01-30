using Venzox.Validation;
using Venzox.Validation.Rules;
using ZXing.Net.MAUI;
using LocalFramework.Services;
using LocalFramework.Models;
using System.Text.Json;

namespace LocalFramework
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public MainPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            RequestCameraPermission();
        }

        private async void RequestCameraPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Camera>();
            }

            if (status != PermissionStatus.Granted)
            {
                // Handle the case where the user denies permission
                await DisplayAlert("Permission Denied", "Camera permission is required to scan QR codes.", "OK");
            }
        }

        private async void BarcodeReaderView_OnResultFound(object sender, BarcodeDetectionEventArgs e)
        {
            if (e.Results == null || e.Results.Length == 0)
            {
                return;
            }

            string scannedCode = e.Results[0].Value;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                resultLabel.Text = $"Scanned: {scannedCode}";

                var validator = new QrCodeValidator();
                var validationResult = validator.Validate(new QrCodeData { Code = scannedCode });

                var qrCodeRecord = new QrCodeRecord
                {
                    Code = scannedCode,
                    IsValid = validationResult.IsValid,
                    ScanDate = DateTime.Now
                };

                if (validationResult.IsValid)
                {
                    resultLabel.Text += "\nValidation: Passed!";
                    resultLabel.TextColor = Colors.Green;
                }
                else
                {
                    resultLabel.Text += "\nValidation: Failed!";
                    resultLabel.TextColor = Colors.Red;
                    qrCodeRecord.ValidationErrors = JsonSerializer.Serialize(validationResult.Errors.Select(err => err.ErrorMessage));
                    foreach (var error in validationResult.Errors)
                    {
                        resultLabel.Text += $"\n - {error.ErrorMessage}";
                    }
                }

                await _databaseService.SaveQrCodeRecordAsync(qrCodeRecord);
            });
        }
    }

    // Dummy class for QR Code data
    public class QrCodeData
    {
        public string? Code { get; set; }
    }

    // Dummy validator for QR Code data
    public class QrCodeValidator : Validator<QrCodeData>
    {
        public QrCodeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().Must(BeAValidQrCode, "QR Code must contain 'Venzox'.");
        }

        private bool BeAValidQrCode(string? code)
        {
            // Here you would implement your specific strong validation logic for QR codes
            // For demonstration, let's say a valid QR code must contain "Venzox"
            return code != null && code.Contains("Venzox");
        }
    }
}
