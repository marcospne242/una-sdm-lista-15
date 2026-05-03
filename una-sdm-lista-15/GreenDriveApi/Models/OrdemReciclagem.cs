namespace GreenDriveApi.Models;

public class OrdemReciclagem
{
    public int Id { get; set; }
    public int BateriaId { get; set; }
    public Bateria? Bateria { get; set; }
    public int EstacaoId { get; set; }
    public EstacaoCarga? Estacao { get; set; }
    public string Prioridade { get; set; } = "Baixa"; // [Baixa, Alta, Critica]
    public decimal CustoProcessamento { get; set; }
}