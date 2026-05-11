using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class RepositorioEstoqueEntradaEmArquivo
{
    protected ContextoJson contexto;
    protected List<EstoqueEntrada> registros;

    public RepositorioEstoqueEntradaEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;

        this.registros = CarregarRegistros();
    }

    public List<EstoqueEntrada> CarregarRegistros()
    {
        return registros;
    }
}
