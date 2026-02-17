namespace AgroSolutions.Identity.Web.Application.DTOs;

public class FazendaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public Guid ProdutorId { get; set; }
}

public class TalhaoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty; 
    public double AreaHectares { get; set; }
    public Guid FazendaId { get; set; }
}

public class SensorDto
{
    public Guid Id { get; set; }
    public string Identificador { get; set; } = string.Empty; 
    public string TipoSensor { get; set; } = string.Empty; 
    public Guid TalhaoId { get; set; }
}