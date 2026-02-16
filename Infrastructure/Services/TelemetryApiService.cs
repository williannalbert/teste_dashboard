using AgroSolutions.Identity.Web.Application.Interfaces;
using AgroSolutions.Identity.Web.Domain.Models;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace AgroSolutions.Identity.Web.Infrastructure.Services;

public class TelemetryApiService : ITelemetryService
{
    private readonly HttpClient _http;

    public TelemetryApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Telemetry>> GetHistoryAsync(string sensorId, int hoursBack)
    {
        try
        {
            var startDate = DateTime.UtcNow.AddHours(-hoursBack).ToString("yyyy-MM-ddTHH:mm:ssZ");
            var url = $"api/History?sensor_id={sensorId}&start_date={startDate}";

            var jsonArray = await _http.GetFromJsonAsync<JsonNode[]>(url);

            if (jsonArray == null) return new List<Telemetry>();

            var result = new List<Telemetry>();

            foreach (var item in jsonArray)
            {
                var t = new Telemetry
                {
                    Id = item["id"]?.ToString() ?? "",
                    SensorId = item["sensorId"]?.ToString() ?? "",
                    Timestamp = item["timestamp"]?.GetValue<DateTime>() ?? DateTime.MinValue,
                    FieldId = item["fieldId"]?.GetValue<Guid>() ?? Guid.Empty,
                    Type = item["type"]?.ToString() ?? "",
                    Email = item["email"]?.ToString()
                };

                var data = item["data"];
                if (data != null)
                {
                    t.SoilMoisturePercent = (double?)data["soilMoisturePercent"];
                    t.SoilPh = (double?)data["soilPh"];
                    if (data["nutrients"] is JsonNode nut)
                    {
                        t.Nitrogen = (double?)nut["nitrogen"];
                        t.Phosphorus = (double?)nut["phosphorus"];
                        t.Potassium = (double?)nut["potassium"];
                    }

                    t.TempCelsius = (double?)data["tempCelsius"];
                    t.HumidityPercent = (double?)data["humidityPercent"];
                    t.WindSpeedKmh = (double?)data["windSpeedKmh"];
                    t.RainMmLastHour = (double?)data["rainMmLastHour"];
                    t.WindDirection = data["windDirection"]?.ToString();
                    t.DewPoint = (double?)data["dewPoint"];

                    t.FillLevelPercent = (double?)data["fillLevelPercent"];
                    t.Co2Ppm = (double?)data["co2Ppm"];
                    t.AvgTempCelsius = (double?)data["avgTempCelsius"];
                }

                result.Add(t);
            }

            return result.OrderByDescending(x => x.Timestamp).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar dados da API: {ex.Message}");
            return new List<Telemetry>();
        }
    }
}