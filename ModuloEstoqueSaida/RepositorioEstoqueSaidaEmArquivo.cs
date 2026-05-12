using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoqueSaida;

public class RepositorioEstoqueSaidaEmArquivo : RepositorioBaseEmArquivo<EstoqueSaida>, IRepositorio<EstoqueSaida>
{
    public RepositorioEstoqueSaidaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<EstoqueSaida> CarregarRegistros()
    {
        return contexto.RegistrosSaida;
    }
}