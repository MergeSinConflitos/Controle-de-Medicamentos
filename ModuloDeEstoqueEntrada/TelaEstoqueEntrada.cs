using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.Ultilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class TelaEstoqueEntrada : ITelaOpcoes, ITelaEstoque
{
    public readonly IRepositorio<EstoqueEntrada> repositorioEstoqueEntrada;
    public readonly IRepositorio<Medicamento> repositorioMedicamento;
    public readonly IRepositorio<Funcionario> repositorioFuncionario;
    public TelaEstoqueEntrada(IRepositorio<EstoqueEntrada> repositorioEstoqueEntrada, IRepositorio<Medicamento> repositorioMedicamento, IRepositorio<Funcionario> repositorioFuncionario)
    {
        this.repositorioEstoqueEntrada = repositorioEstoqueEntrada;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public string? ObterOpcaoMenu()
    {

        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Estoque");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar Registro de Entrada");
        Console.WriteLine("2 - Visualizar Registros de Entrada");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Cadastrar()
    {
        Console.Clear();

        Console.WriteLine("-----------------------------------------------");
        System.Console.WriteLine("Cadastrar requisito");
        Console.WriteLine("-----------------------------------------------");

        try
        {
            EstoqueEntrada novoRequisito = ObterDadosCadastrais();

            List<string> erros = novoRequisito.Validar();

            if (erros.Count > 0)
            {
                Notificador.ExibirMensagensErro(erros);
            }

            repositorioEstoqueEntrada.Cadastrar(novoRequisito);

            Notificador.ExibirMensagem($"O registro \"{novoRequisito.Id}\" foi cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Notificador.ExibirMensagem("O formato do valor de um dos campos está inválido.");
            Cadastrar();
        }


        catch (Exception)
        {
            Notificador.ExibirMensagem("Ocorreu um erro inesperado. Tente novamente.");
            Cadastrar();
        }

    }



    public void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine("Visualizar Requisitos de Entrada");
            Console.WriteLine("-----------------------------------------------");

        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20} | {4, -20}",
            "Id", "Funcionario", "Medicamento", "Quantidade", "Data"
        );

        List<EstoqueEntrada> requisitos = repositorioEstoqueEntrada.SelecionarTodos();


        if (requisitos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum requisito cadastrado");
            return;
        }

        foreach (EstoqueEntrada e in requisitos)
        {
            Console.WriteLine("{0,-7} | {1,-20} | {2,-20} | {3,-20} | {4, -20}", e.Id, e.Funcionario.Nome, e.Medicamento.Nome, e.Quantidade, e.Data.ToShortDateString());
        }


        if (deveExibirCabecalho)
        {

            System.Console.WriteLine("--------------------------------------");
            System.Console.WriteLine("Pressione ENTER para continuar");
            Console.ReadLine();
        }
    }

    protected EstoqueEntrada ObterDadosCadastrais()
    {
        Funcionario funcionarioSelecionado = SelecionarFuncionario();
        Medicamento medicamentoSelecionado = SelecionarMedicamento();

        int quantidadeRequisitada;
        Console.Write("informe a quantidade do medicamento requisitado:");
        int.TryParse(Console.ReadLine(), out quantidadeRequisitada);

        return new EstoqueEntrada(medicamentoSelecionado, funcionarioSelecionado, quantidadeRequisitada);
    }

    private Medicamento SelecionarMedicamento()
    {
        var medicamentos = repositorioMedicamento.SelecionarTodos();

        System.Console.WriteLine("--------------------------------------------------------------------------");
        System.Console.WriteLine("{0, -7} | {1, -20} | {2, -20} | {3, -20}", "Id", "Nome", "Descrição", "Quantidade");
        System.Console.WriteLine("--------------------------------------------------------------------------");
        System.Collections.IList list = medicamentos;
        for (int i = 0; i < list.Count; i++)
        {
            Medicamento m = (Medicamento)list[i];
            System.Console.WriteLine("{0, -7} | {1, -20} | {2, -20} | {3, -20}", m.Id, m.Nome, m.Descricao, m.QuantidadeEmEstoque);
            System.Console.WriteLine("--------------------------------------------------------------------------");
        }

        string idMedicamento;

        do
        {
            System.Console.Write("Informe o id do medicamento desejado: ");
            idMedicamento = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(idMedicamento) && idMedicamento.Length == 7)
            {
                break;
            }

            Notificador.ExibirMensagem("Id inválido. O Id deve ter 7 caracteres.");
        } while (true);

        Medicamento medicamentoSelecionado = null;

        for (int i = 0; i < list.Count; i++)
        {
            Medicamento m = (Medicamento)list[i];
            if (m.Id == idMedicamento)
            {
                medicamentoSelecionado = m;
                break;
            }
        }
        if (medicamentoSelecionado == null)
        {
            Notificador.ExibirMensagem("Medicamento não encontrado.");
            return null;
        }

        return medicamentoSelecionado;

    }

    private Funcionario SelecionarFuncionario()
    {
        var funcionarios = repositorioFuncionario.SelecionarTodos();

        System.Console.WriteLine("--------------------------------------------------------------------------");
        System.Console.WriteLine("{0, -7} | {1, -20} | {2, -20} | {3, -20}", "Id", "Nome", "Telefone", "CNPJ");
        System.Console.WriteLine("--------------------------------------------------------------------------");
        System.Collections.IList list = funcionarios;
        for (int i = 0; i < list.Count; i++)
        {
            Funcionario f = (Funcionario)list[i];
            System.Console.WriteLine("{0, -7} | {1, -20} | {2, -20} | {3, -20}", f.Id, f.Nome, f.Telefone, f.Cpf);
            System.Console.WriteLine("--------------------------------------------------------------------------");
        }

        string idFuncionario;

        do
        {
            System.Console.Write("Informe o id do funcionario que deseja efetuar a requisição: ");
            idFuncionario = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(idFuncionario) && idFuncionario.Length == 7)
            {
                break;
            }

            Notificador.ExibirMensagem("Id inválido. O Id deve ter 7 caracteres.");
        } while (true);

        Funcionario funcionarioSelecionado = null;

        for (int i = 0; i < list.Count; i++)
        {
            Funcionario f = (Funcionario)list[i];
            if (f.Id == idFuncionario)
            {
                funcionarioSelecionado = f;
                break;
            }
        }
        if (funcionarioSelecionado == null)
        {
            Notificador.ExibirMensagem("Funcionário não encontrado.");
            return null;
        }

        return funcionarioSelecionado;

    }
}
