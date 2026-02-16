using AgroSolutions.Identity.Web.Domain.Models;

namespace AgroSolutions.Identity.Web.Application.Interfaces;

public interface ITelemetryService
{
    Task<List<Telemetry>> GetHistoryAsync(string sensorId, int hoursBack);
}
