using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class RepositorioEstoqueEntradaEmMemoria : IRepositorio<EstoqueEntrada>
{
    protected static List<EstoqueEntrada> registros = new List<EstoqueEntrada>();

    public void Cadastrar(EstoqueEntrada RequisicaoEntrada)
    {
        registros.Add(RequisicaoEntrada);
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
