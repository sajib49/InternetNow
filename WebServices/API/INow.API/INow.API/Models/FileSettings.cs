namespace INow.API.Models
{
    public class FileSettings
    {
        public double FileSizeInKb { get; set; }
        public double? NumericDataPercentage { get; set; }
        public double? AlphanumericDataPercentage { get; set; }
        public double? FloatDataPercentage { get; set; }
    }
}