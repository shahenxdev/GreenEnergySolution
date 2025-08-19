using API.Models;

namespace API.Services
{
    public interface IEmissionService
    {
        void AddEmission(Emission emission);
        EmissionSummary GetSummary();
    }
}
