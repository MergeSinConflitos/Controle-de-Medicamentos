using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Ultilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(IRepositorio<Funcionario> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Funcionarios");

        List<Funcionario> Funcionarios = repositorio.SelecionarTodos();

        if (Funcionarios.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum funcionario registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -10}",
            "Id", "Nome", "Telefone", "CPF"
        );

        foreach (Funcionario f in Funcionarios)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -10}",
                f.Id, f.Nome, f.Telefone, f.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Funcionario ObterDadosCadastrais()
    {
        Console.Write("Informe o nome do Funcionario: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Informe o telefone do Funcionario: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Informe n° do CPF: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Funcionario(nome, telefone, cpf);
    }
}
