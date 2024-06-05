namespace RhitmoTech.Estacionamento
{
    public class Veiculo(string placa, TipoVeiculoEnum tipo)
    {
        public string Placa { get; private set; } = placa;
        public TipoVeiculoEnum Tipo { get; private set; } = tipo;
    }
}