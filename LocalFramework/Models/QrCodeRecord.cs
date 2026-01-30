using SQLite;

namespace LocalFramework.Models
{
    public class QrCodeRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Code { get; set; }
        public bool IsValid { get; set; }
        public string? ValidationErrors { get; set; }
        public DateTime ScanDate { get; set; }
    }
}
