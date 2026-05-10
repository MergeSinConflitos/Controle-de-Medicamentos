using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Ultilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaOpcoes, ITelaCrud
{
    public TelaPaciente(IRepositorio<Paciente> repositorio) : base("Paciente", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Pacientes");

        List<Paciente> pacientes = repositorio.SelecionarTodos();

        if (pacientes.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum paciente registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -10} | {4, -20}",
            "Id", "Nome", "Telefone", "Cartão do SUS", "CPF"
        );

        foreach (Paciente p in pacientes)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -10} | {4, -20}",
                p.Id, p.Nome, p.Telefone, p.CartaoSus, p.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write("Informe o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Informe o telefone do paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;
        

        Console.Write("Informe n° do Cartão do SUS: ");
        string cartaoSus = Console.ReadLine() ?? string.Empty;

        Console.Write("Informe n° do CPF : ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Paciente(nome, telefone, cartaoSus, cpf);
    }

    protected override List<string> ValidarRegistroDuplicado(Paciente novaEntidade, string? idIgnorado = null)
    {
        List<string> erros = new List<string>();

        foreach (Paciente p in repositorio.SelecionarTodos())
        {
            // si estamos editando, ignoramos el registro actual
            if (p.Id == idIgnorado)
                continue;

            if (p.CartaoSus == novaEntidade.CartaoSus)
            {
                erros.Add("Já existe um paciente cadastrado com este Cartão do SUS");
                break;
            }
        }

        return erros;
    }
}
