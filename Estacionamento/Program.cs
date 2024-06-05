
using RhitmoTech.Estacionamento;

Estacionamento estacionamento = new();
Veiculo[] motos = new Veiculo[5];
for (int i = 0; i < motos.Length; i++)
{
    motos[i] = new("MT000" + i, TipoVeiculoEnum.Moto);
}
Veiculo[] carros = new Veiculo[10];
for (int i = 0; i < carros.Length; i++)
{
    carros[i] = new("CRR000" + i, TipoVeiculoEnum.Carro);
}
Veiculo[] vans = new Veiculo[3];
for (int i = 0; i < vans.Length; i++)
{
    vans[i] = new("VAN000" + i, TipoVeiculoEnum.Van);
}

if (estacionamento.EstacionamentoEstaVazio())
    Console.WriteLine(@"O estacionamento está vazio.");

foreach (var moto in motos)
{
    Console.WriteLine($"Tentar estacionar veiculo de placa {moto.Placa}:");
    estacionamento.TentarEstacionarVeiculo(moto);
}

foreach (var carro in carros)
{
    Console.WriteLine($"Tentar estacionar veiculo de placa {carro.Placa}:");
    estacionamento.TentarEstacionarVeiculo(carro);
}

for (int i = 0; i < 6; i++)
{
    Console.WriteLine($"Tentar que veiculo de placa {carros[i].Placa} deixe o estacionamento:");
    estacionamento.TentarDeixarVaga(carros[i]);
}

foreach (var van in vans)
{
    Console.WriteLine($"Tentar estacionar veiculo de placa {van.Placa}:");
    estacionamento.TentarEstacionarVeiculo(van);
}

Console.WriteLine($"Esvaziar o estacionamento:");
foreach (var moto in motos)
    estacionamento.TentarDeixarVaga(moto);
foreach (var carro in carros)
    estacionamento.TentarDeixarVaga(carro);
foreach (var van in vans)
    estacionamento.TentarDeixarVaga(van);



