namespace RhitmoTech.Estacionamento
{
    public class Vaga(TipoVeiculoEnum tipoVeiculo)
    {
        public TipoVeiculoEnum Tipo { get; } = tipoVeiculo;
        public bool Disponivel { get; set; } = true;
        public Veiculo? VeiculoEstacionado { get; set; }

        public void Desocupar()
        {
            Disponivel = true;
            VeiculoEstacionado = null;
        }

        public void Ocupar(Veiculo veiculo)
        {
            if (!Disponivel)
                throw new Exception("Vaga indisponível.");

            if (Tipo == TipoVeiculoEnum.Moto && veiculo.Tipo != Tipo)
                throw new Exception("Este veiculo não pode estacionar nesta vaga.");

            Disponivel = false;
            VeiculoEstacionado = veiculo;
        }
    }
}