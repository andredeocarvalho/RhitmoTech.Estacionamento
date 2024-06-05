using RhitmoTech.Estacionamento;

namespace RhitmoTech.Test
{
    public class EstacionamentoTests
    {
        [Fact]
        public void ContarVagasDisponiveis_ShouldReturnCorrectCount()
        {
            // Arrange
            var estacionamento = new Estacionamento.Estacionamento(1,1,1);

            // Act
            var result = estacionamento.ContarVagasDisponiveis();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void ContarVagasTotais_ShouldReturnCorrectCount()
        {
            // Arrange
            var estacionamento = new Estacionamento.Estacionamento(1,1,1);

            // Act
            var result = estacionamento.ContarVagasTotais();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void EstacionamentoEstaCheio_ShouldReturnTrue_WhenNoVagasDisponiveis()
        {
            // Arrange
            var estacionamento = new Estacionamento.Estacionamento(0, 0, 0);

            // Act
            var result = estacionamento.EstacionamentoEstaCheio();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EstacionamentoEstaVazio_ShouldReturnTrue_WhenAllVagasDisponiveis()
        {
            // Arrange
            var estacionamento = new Estacionamento.Estacionamento(1, 1, 1);

            // Act
            var result = estacionamento.EstacionamentoEstaVazio();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VagasTipoVeiculoCheias_ShouldReturnTrue_WhenAllVagasOcupadas()
        {
            // Arrange
            var estacionamento = new Estacionamento.Estacionamento(1, 1, 1);
            var veiculoMoto = new Veiculo("MT-0000", TipoVeiculoEnum.Moto);
            var veiculoCarro = new Veiculo("CRR-0000", TipoVeiculoEnum.Carro);
            var veiculoVan = new Veiculo("VAN-0000", TipoVeiculoEnum.Van);

            // Act
            estacionamento.TentarEstacionarVeiculo(veiculoMoto);
            estacionamento.TentarEstacionarVeiculo(veiculoCarro);
            estacionamento.TentarEstacionarVeiculo(veiculoVan);
            var resultMoto = estacionamento.VagasTipoVeiculoCheias(TipoVeiculoEnum.Moto);
            var resultCarro = estacionamento.VagasTipoVeiculoCheias(TipoVeiculoEnum.Carro);
            var resultVan = estacionamento.VagasTipoVeiculoCheias(TipoVeiculoEnum.Van);

            // Assert
            Assert.True(resultMoto);
            Assert.True(resultCarro);
            Assert.True(resultVan);
        }

        [Fact]
        public void ContarVagasOcupadasPorTipo_ShouldReturnCorrectCount()
        {
            // Arrange
            var estacionamento = new Estacionamento.Estacionamento(2, 2, 2);
            var veiculoMoto = new Veiculo ("MT-0000", TipoVeiculoEnum.Moto);
            var veiculoCarro = new Veiculo ("CRR-0000", TipoVeiculoEnum.Carro);
            var veiculoVan = new Veiculo ("VAN-0000", TipoVeiculoEnum.Van);

            // Act
            estacionamento.TentarEstacionarVeiculo(veiculoMoto);
            estacionamento.TentarEstacionarVeiculo(veiculoCarro);
            estacionamento.TentarEstacionarVeiculo(veiculoVan);
            var resultMoto = estacionamento.ContarVagasOcupadasPorTipo(TipoVeiculoEnum.Moto);
            var resultCarro = estacionamento.ContarVagasOcupadasPorTipo(TipoVeiculoEnum.Carro);
            var resultVan = estacionamento.ContarVagasOcupadasPorTipo(TipoVeiculoEnum.Van);

            // Assert
            Assert.Equal(1, resultMoto);
            Assert.Equal(1, resultCarro);
            Assert.Equal(1, resultVan);
        }
    }
}