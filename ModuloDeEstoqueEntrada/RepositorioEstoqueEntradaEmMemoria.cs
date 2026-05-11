using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class RepositorioEstoqueEntradaEmMemoria
{
    protected static List<EstoqueEntrada> registros = new List<EstoqueEntrada>();

    public void Cadastrar(EstoqueEntrada RequisicaoEntrada)
    {
        registros.Add(RequisicaoEntrada);
    }

    public List<EstoqueEntrada> SelecionarTodos()
    {
        return registros;
    }
}
