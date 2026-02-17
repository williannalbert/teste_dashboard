using AgroSolutions.Identity.Web.Application.DTOs;
using AgroSolutions.Identity.Web.Application.Interfaces;

namespace AgroSolutions.Identity.Web.Infrastructure.Services;

public class PropertiesServiceMock : IPropertiesService
{
    private readonly Guid _fazendaId = Guid.Parse("f1a2b3c4-d5e6-4789-0123-456789abcdef");
    private readonly Guid _talhaoId = Guid.Parse("a1b2c3d4-e5f6-7890-1234-567890abcdef"); 

    public async Task<List<FazendaDto>> GetFazendasByProdutorAsync()
    {
        await Task.Delay(300); 
        return new List<FazendaDto>
        {
            new FazendaDto
            {
                Id = _fazendaId,
                Nome = "Fazenda Sol Nascente",
                Endereco = "Rodovia SP-304, km 120"
            },
            new FazendaDto
            {
                Id = Guid.NewGuid(),
                Nome = "Sítio Vista Alegre",
                Endereco = "Estrada Rural, s/n"
            }
        };
    }

    public async Task<List<TalhaoDto>> GetTalhoesByFazendaAsync(Guid fazendaId)
    {
        await Task.Delay(300);

        if (fazendaId == _fazendaId)
        {
            return new List<TalhaoDto>
            {
                new TalhaoDto
                {
                    Id = _talhaoId,
                    Nome = "Talhão Norte (Soja)",
                    FazendaId = fazendaId,
                    AreaHectares = 50.5
                },
                new TalhaoDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "Talhão Sul (Milho)",
                    FazendaId = fazendaId,
                    AreaHectares = 30.0
                }
            };
        }

        return new List<TalhaoDto>(); 
    }

    public async Task<List<SensorDto>> GetSensoresByTalhaoAsync(Guid talhaoId)
    {
        await Task.Delay(300);

        if (talhaoId == _talhaoId)
        {
            return new List<SensorDto>
            {
                new SensorDto
                {
                    Id = Guid.Parse("03ccb3d7-3c82-40aa-b76d-877964aa3b50"),
                    Identificador = "Estação Met. Alpha",
                    TipoSensor = "Meteorologica", 
                    TalhaoId = talhaoId
                },
                new SensorDto
                {
                    Id = Guid.Parse("40d29177-8a49-4607-af25-36779b4f0d08"),
                    Identificador = "Silo Principal 01",
                    TipoSensor = "Silo",
                    TalhaoId = talhaoId
                },
                new SensorDto
                {
                    Id = Guid.Parse("d3b07384-d9a1-4d43-9833-28151b753703"),
                    Identificador = "Sensor Solo P1",
                    TipoSensor = "Solo",
                    TalhaoId = talhaoId
                }
            };
        }

        return new List<SensorDto>();
    }
}
