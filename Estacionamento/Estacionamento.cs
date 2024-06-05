namespace RhitmoTech.Estacionamento
{
    public class Estacionamento
    {
        private Vaga[][] Vagas { get; }
        private Dictionary<string, Vaga> VeiculosEstacionados { get; }
        private Dictionary<string, Vaga[]> VansEstacionadas { get; }
        private int CapacidadeTotal { get; }

        public Estacionamento(int capacidadeMoto = 4, int capacidadeCarro = 10, int capacidadeVan = 2)
        {
            Vagas = new Vaga[3][];
            Vagas[0] = new Vaga[capacidadeMoto];
            for (var i = 0; i < capacidadeMoto; i++)
                Vagas[0][i] = new Vaga(TipoVeiculoEnum.Moto);
            Vagas[1] = new Vaga[capacidadeCarro];
            for (var i = 0; i < capacidadeCarro; i++)
                Vagas[1][i] = new Vaga(TipoVeiculoEnum.Carro);
            Vagas[2] = new Vaga[capacidadeVan];
            for (var i = 0; i < capacidadeVan; i++)
                Vagas[2][i] = new Vaga(TipoVeiculoEnum.Van);
            VeiculosEstacionados = [];
            VansEstacionadas = [];
            CapacidadeTotal = capacidadeMoto + capacidadeCarro + capacidadeVan;
        }

        public int ContarVagasDisponiveis() => Vagas[0].Count(v => v.Disponivel) + Vagas[1].Count(v => v.Disponivel) + Vagas[2].Count(v => v.Disponivel);

        public int ContarVagasTotais() => CapacidadeTotal;

        public bool EstacionamentoEstaCheio() => ContarVagasDisponiveis() == 0;

        public bool EstacionamentoEstaVazio() => ContarVagasDisponiveis() == CapacidadeTotal;

        public bool VagasTipoVeiculoCheias(TipoVeiculoEnum tipo) => Vagas[(int)tipo].All(vaga => !vaga.Disponivel);

        public int ContarVagasOcupadasPorTipo(TipoVeiculoEnum tipo) => Vagas[(int)tipo].Count(vaga => !vaga.Disponivel);

        public void TentarEstacionarVeiculo(Veiculo veiculo)
        {
            if (EstacionamentoEstaCheio())
            {
                Console.WriteLine("O estacionamento está lotado.");
                return;
            }

            if (VeiculosEstacionados.ContainsKey(veiculo.Placa) || VansEstacionadas.ContainsKey(veiculo.Placa))
            {
                Console.WriteLine($"O veículo de placa {veiculo.Placa} já se encontra no estacionamento.");
                return;
            }

            bool estacionado = veiculo.Tipo switch
            {
                TipoVeiculoEnum.Moto => EstacionarMoto(veiculo),
                TipoVeiculoEnum.Carro => EstacionarCarro(veiculo),
                TipoVeiculoEnum.Van => EstacionarVan(veiculo),
            };

            if (!estacionado)
                Console.WriteLine($"Não foi possível estacionar o veículo de placa {veiculo.Placa}.");

            if (EstacionamentoEstaCheio())
            {
                Console.WriteLine("O estacionamento ficou lotado.");
                return;
            }
        }

        public void TentarDeixarVaga(Veiculo veiculo)
        {
            if (VeiculosEstacionados.TryGetValue(veiculo.Placa, out Vaga? vaga))
            {
                vaga.Desocupar();
                VeiculosEstacionados.Remove(veiculo.Placa);
                Console.WriteLine($"O veículo de placa {veiculo.Placa} deixou o estacionamento.");
            }
            else if (veiculo.Tipo == TipoVeiculoEnum.Van && VansEstacionadas.TryGetValue(veiculo.Placa, out Vaga[]? vagas))
            {
                foreach (var v in vagas)
                    v.Desocupar();
                VansEstacionadas.Remove(veiculo.Placa);
                Console.WriteLine($"O veículo de placa {veiculo.Placa} deixou o estacionamento.");
            }
            else
                Console.WriteLine($"O veículo de placa {veiculo.Placa} não se encontra no estacionamento.");

            if (EstacionamentoEstaVazio())
                Console.WriteLine("O estacionamento está vazio.");
        }

        #region Estacionar
        private bool EstacionarMoto(Veiculo veiculo) =>
            EstacionarEmVagaDeTipoVeiculo(veiculo, TipoVeiculoEnum.Moto) ||
            EstacionarEmVagaDeTipoVeiculo(veiculo, TipoVeiculoEnum.Carro) ||
            EstacionarEmVagaDeTipoVeiculo(veiculo, TipoVeiculoEnum.Van);

        private bool EstacionarCarro(Veiculo veiculo) =>
            EstacionarEmVagaDeTipoVeiculo(veiculo, TipoVeiculoEnum.Carro) || EstacionarEmVagaDeTipoVeiculo(veiculo, TipoVeiculoEnum.Van);

        private bool EstacionarVan(Veiculo veiculo)
        {
            if (EstacionarEmVagaDeTipoVeiculo(veiculo, TipoVeiculoEnum.Van))
                return true;

            if (veiculo.Tipo != TipoVeiculoEnum.Van)
                return false;

            for (int i = 0; i < Vagas[(int)TipoVeiculoEnum.Carro].Length - 2; i++)
            {
                if (Vagas[(int)TipoVeiculoEnum.Carro][i].Disponivel &&
                    Vagas[(int)TipoVeiculoEnum.Carro][i + 1].Disponivel &&
                    Vagas[(int)TipoVeiculoEnum.Carro][i + 2].Disponivel)
                {
                    Vagas[(int)TipoVeiculoEnum.Carro][i].Ocupar(veiculo);
                    Vagas[(int)TipoVeiculoEnum.Carro][i + 1].Ocupar(veiculo);
                    Vagas[(int)TipoVeiculoEnum.Carro][i + 2].Ocupar(veiculo);
                    VansEstacionadas.Add(veiculo.Placa, [Vagas[(int)TipoVeiculoEnum.Carro][i], Vagas[(int)TipoVeiculoEnum.Carro][i + 1], Vagas[(int)TipoVeiculoEnum.Carro][i + 2]]);
                    Console.WriteLine($"O veículo de placa {veiculo.Placa} ocupou três vagas de {TipoVeiculoEnum.Carro}.");
                    return true;
                }
            }

            return false;
        }

        private bool EstacionarEmVagaDeTipoVeiculo(Veiculo veiculo, TipoVeiculoEnum tipoVeiculo)
        {
            var vagaDisponivel = Vagas[(int)tipoVeiculo].FirstOrDefault(v => v.Disponivel);
            if (vagaDisponivel == null)
                return false;

            vagaDisponivel.Ocupar(veiculo);
            VeiculosEstacionados.Add(veiculo.Placa, vagaDisponivel);
            Console.WriteLine($"O veículo de placa {veiculo.Placa} ocupou uma vaga de {tipoVeiculo}.");
            return true;
        }
        #endregion

    }
}