using System;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public interface ITelaEstoque
{
    public void Cadastrar();
    public void VisualizarTodos(bool deveExibirCabecalho);
}
