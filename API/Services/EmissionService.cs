using API.Models;

namespace API.Services
{
    public class EmissionService : IEmissionService
    {
        private readonly List<Emission> _emissions = new();
        public void AddEmission(Emission emission)
        {
            _emissions.Add(emission);
        }

        public EmissionSummary GetSummary()
        {
            return new EmissionSummary
            {
                Energy = _emissions.Where(e => e.Type == "Energy").Sum(e => e.Amount),
                Transport = _emissions.Where(e => e.Type == "Transport").Sum(e => e.Amount),
                Waste = _emissions.Where(e => e.Type == "Waste").Sum(e => e.Amount)
            };
        }
    }
}
