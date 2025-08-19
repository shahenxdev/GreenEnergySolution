namespace API.Models
{
    public class Emission
    {
        public string Type { get; set; } = string.Empty; // Energy, Transport, Waste
        public double Amount { get; set; }               // CO2 in kg
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
