using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Ultilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoqueSaida;

public class TelaEstoqueSaida : TelaBase<EstoqueSaida>, ITelaCrud, ITelaOpcoes
{   
    private IRepositorio<Paciente> repositorioPaciente;
    private IRepositorio<Medicamento> repositorioMedicamento;
    
    public TelaEstoqueSaida(
        IRepositorio<EstoqueSaida> repositorioSaida,
        IRepositorio<Paciente> repositorioPaciente,
        IRepositorio<Medicamento> repositorioMedicamento
        
    ) : base("Requisição de Saída", repositorioSaida)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioMedicamento = repositorioMedicamento;
    }
    
    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Requisições de Saída");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar requisição de saída");
        Console.WriteLine("2 - Visualizar requisições de saída");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
    }
    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Requisições de Saída");

        List<EstoqueSaida> saidas = repositorio.SelecionarTodos();

        if (saidas.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhuma requisição de saída registrada.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -12} | {2, -30} | {3, -30}",
            "Id", "Data", "Paciente", "Medicamentos"
        );

        foreach (EstoqueSaida s in saidas)
        {
            //lista de medicamentos em uma string
            string medicamentos = "";

            foreach (Medicamento m in s.Medicamentos)
            {
                if (medicamentos != "")
                    medicamentos += ", ";

                medicamentos += m.Nome;
            }

            Console.WriteLine(
                "{0, -7} | {1, -12} | {2, -30} | {3, -30}",
                s.Id,
                s.Data.ToShortDateString(),
                s.Paciente.Nome,
                medicamentos
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override EstoqueSaida ObterDadosCadastrais()
    {
        Console.Write("Informe a data da requisição: ");
        DateTime data = Convert.ToDateTime(Console.ReadLine());

        // mostrar y seleccionar paciente
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Pacientes disponíveis:");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("{0, -7} | {1, -30} | {2, -15}", "Id", "Nome", "CPF");

        foreach (Paciente p in repositorioPaciente.SelecionarTodos())
        {
            Console.WriteLine("{0, -7} | {1, -30} | {2, -15}", p.Id, p.Nome, p.Cpf);
        }

        string? idPaciente;
        do
        {   
            
            Console.WriteLine("Digite o ID do paciente (ou S para sair): ");
            idPaciente = Console.ReadLine() ?? string.Empty;

            if(idPaciente.ToUpper() == "S")
                return null!;

            if (idPaciente.Length == 7)
                break;

        } while (true);

        Paciente? pacienteSelecionado = repositorioPaciente.SelecionarPorId(idPaciente);

        if (pacienteSelecionado == null)
        {
            Notificador.ExibirMensagem("Paciente não encontrado.");
            return ObterDadosCadastrais();
        }

        // mostrar y seleccionar medicamentos
        List<Medicamento> medicamentosSelecionados = new List<Medicamento>();

        bool continuarSelecionando = true;

        while (continuarSelecionando)
        {   
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Medicamentos disponíveis:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -10}",
                "Id", "Nome", "Estoque"
            );

            foreach (Medicamento m in repositorioMedicamento.SelecionarTodos())
            {
                // destaca medicamentos com menos de 20 unidades
                if (m.QuantidadeEmEstoque < 20)
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(
                    "{0, -7} | {1, -30} | {2, -10}",
                    m.Id, m.Nome, m.QuantidadeEmEstoque
                );

                Console.ResetColor();
            }

            string? idMedicamento;
            do
            {
                Console.WriteLine("Digite o ID do medicamento (ou S para finalizar): ");
                idMedicamento = Console.ReadLine() ?? string.Empty;

                if(idMedicamento.ToUpper() == "S")
                {
                    continuarSelecionando = false;
                    break;
                }

                if (idMedicamento.Length == 7)
                    break;

            } while (true);

            if (!continuarSelecionando)
                break;

            Medicamento? medicamentoSelecionado = repositorioMedicamento.SelecionarPorId(idMedicamento);

            if (medicamentoSelecionado == null)
            {
                Notificador.ExibirMensagem("Medicamento não encontrado.");
                continue;
            }

            // validar stock suficiente
            if (medicamentoSelecionado.QuantidadeEmEstoque <= 0)
            {
                Notificador.ExibirMensagem($"Medicamento \"{medicamentoSelecionado.Nome}\" sem estoque!");
                continue;
            }

            // pedir cantidad
            Console.Write($"Informe a quantidade de \"{medicamentoSelecionado.Nome}\": ");
            int quantidade = Convert.ToInt32(Console.ReadLine());

            if (quantidade <= 0)
            {
                Notificador.ExibirMensagem("A quantidade deve ser um número positivo.");
                continue;
            }

            if (quantidade > medicamentoSelecionado.QuantidadeEmEstoque)
            {
                Notificador.ExibirMensagem(
                    $"Estoque insuficiente! Disponível: {medicamentoSelecionado.QuantidadeEmEstoque}"
                );
                continue;
            }

            // subtraer del stock
            medicamentoSelecionado.QuantidadeEmEstoque -= quantidade;

            medicamentosSelecionados.Add(medicamentoSelecionado);

            Console.WriteLine($"Medicamento \"{medicamentoSelecionado.Nome}\" adicionado!");
        }

        return new EstoqueSaida(data, pacienteSelecionado, medicamentosSelecionados);
  
    }
}
