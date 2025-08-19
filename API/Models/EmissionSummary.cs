namespace API.Models
{
    public class EmissionSummary
    {
        public double Energy { get; set; }
        public double Transport { get; set; }
        public double Waste { get; set; }
        public double Total => Energy + Transport + Waste;
    }
}
