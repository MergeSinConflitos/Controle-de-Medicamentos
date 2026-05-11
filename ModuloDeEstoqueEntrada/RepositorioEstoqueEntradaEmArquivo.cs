using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class RepositorioEstoqueEntradaEmArquivo : IRepositorio<EstoqueEntrada>
{
    protected ContextoJson contexto;
    protected List<EstoqueEntrada> registros;

    public RepositorioEstoqueEntradaEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;

        this.registros = CarregarRegistros();

        this.registros = contexto.RegistrosEntrada;
    }

    public void Cadastrar(EstoqueEntrada entidade)
    {
        registros.Add(entidade);

        contexto.Salvar();
    }

    public List<EstoqueEntrada> CarregarRegistros()
    {
        return registros;
    }

    public bool Editar(string idSelecionado, EstoqueEntrada entidadeAtualizada)
    {
        return false;
    }

    public bool Excluir(EstoqueEntrada registro)
    {
        return false;
    }

    public EstoqueEntrada? SelecionarPorId(string idSelecionado)
    {
        return null;
    }

    public List<EstoqueEntrada> SelecionarTodos()
    {
        return registros;
    }
}
