using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.Ultilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud, ITelaOpcoes
{
    private readonly IRepositorio<Fornecedor> repositorioFornecedor;
    public TelaMedicamento(IRepositorio<Medicamento> repositorio, IRepositorio<Fornecedor> repositorioFornecedor) : base("Medicamentos", repositorio)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {

            ExibirCabecalho("Visualizar Medicamentos");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
            "Id", "Nome", "Descrição", "Quantidade", "Fornecedor"
        );

        List<Medicamento> medicamentos = repositorio.SelecionarTodos();


        if (medicamentos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum medicamento cadastrado");
            return;
        }

        foreach (Medicamento m in medicamentos)
        {
            Console.WriteLine("{0,-7} | {1,-20} | {2,-20} | {3,-20} | {4, -20}", m.Id, m.Nome, m.Descricao, m.QuantidadeEmEstoque, m.Fornecedor.Nome);
        }


        if (deveExibirCabecalho)
        {

            System.Console.WriteLine("--------------------------------------");
            System.Console.WriteLine("Pressione ENTER para continuar");
            Console.ReadLine();
        }

    }


    protected override Medicamento ObterDadosCadastrais()
    {
        System.Console.Write("Informe o nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        System.Console.Write("Informe a descrição: ");
        string descricao = Console.ReadLine() ?? string.Empty;

        VisualizarTodosFornecedores();

        string idFornecedor;

        do
        {
            System.Console.Write("Informe o id do fornecedor do medicamento: ");
            idFornecedor = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(idFornecedor) && idFornecedor.Length == 7)
            {
                break;
            }
        } while (true);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarPorId(idFornecedor);

        return new Medicamento(nome, descricao, fornecedorSelecionado);
    }

    private void VisualizarTodosFornecedores()
    {
        var fornecedores = repositorioFornecedor.SelecionarTodos();
        System.Console.WriteLine("--------------------------------------------------------------------------");
        System.Console.WriteLine("{0, -7} | {1, -20} | {2, -20} | {3, -20}", "Id", "Nome", "Telefone", "CNPJ");
        System.Console.WriteLine("--------------------------------------------------------------------------");
        foreach (Fornecedor f in fornecedores)
        {
            System.Console.WriteLine("{0, -7} | {1, -20} | {2, -20} | {3, -20}", f.Id, f.Nome, f.Telefone, f.CNPJ);
            System.Console.WriteLine("--------------------------------------------------------------------------");

        }


    }

    protected override List<string> ValidarRegistroDuplicado(Medicamento novaEntidade, string? idIgnorado = null)
    {
        List<Medicamento> todosOsMedicamentos = repositorio.SelecionarTodos();
        List<string> erros = new List<string>();

        Medicamento medicamentoExistente = todosOsMedicamentos
            .FirstOrDefault(m => m.Nome == novaEntidade.Nome);

        if (medicamentoExistente != null)
        {
            medicamentoExistente.QuantidadeEmEstoque += novaEntidade.QuantidadeEmEstoque;
            repositorio.Editar(medicamentoExistente.Id, medicamentoExistente);
            erros.Add("Medicamento ja existe,quantidade em estoque foi somada a quantidade existeste");
            return erros;
        }
        else
        {

            return erros;
        }
    }


}

