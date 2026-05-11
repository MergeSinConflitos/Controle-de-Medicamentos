using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Ultilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud, ITelaOpcoes
{
    public TelaFornecedor(IRepositorio<Fornecedor> repositorio) : base("Fornecedor", repositorio)
    {

    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {

            ExibirCabecalho("Visualizar Fornecedores");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();


        if (fornecedores.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum fornecedor cadastrado");
            return;
        }

        foreach (Fornecedor f in fornecedores)
        {
            Console.WriteLine("{0,-7} | {1,-20} | {2,-20} | {3,-20}", f.Id, f.Nome, f.Telefone, f.CNPJ);
        }


        if (deveExibirCabecalho)
        {

            System.Console.WriteLine("--------------------------------------");
            System.Console.WriteLine("Pressione ENTER para continuar");
            Console.ReadLine();
        }


    }

    protected override Fornecedor ObterDadosCadastrais()
    {
        System.Console.Write("Informe o nome do fornecedor: ");
        string nome = Console.ReadLine() ?? string.Empty;

        System.Console.Write("Informe o telefone do fornecedor: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        System.Console.Write("Informe o CNPJ do fornecedor: ");
        string cnpj = Console.ReadLine() ?? string.Empty;

        return new Fornecedor(nome, telefone, cnpj);
    }

    protected override List<string> ValidarRegistroDuplicado(Fornecedor novaEntidade, string? idIgnorado = null)
    {
        List<Fornecedor> todosOsFornecedores = repositorio.SelecionarTodos();
        List<string> erros = new List<string>();

        bool existeCNPJIgual = todosOsFornecedores.Any(f => f.CNPJ == novaEntidade.CNPJ && f.Id != idIgnorado);

        if (existeCNPJIgual)
        {
            erros.Add("Já existe um fornecedor com esse CNPJ");
            return erros;
        }
        return erros;
    }

}
