using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.Ultilidades;

public class TelaPrincipal
{   
    private readonly IRepositorio<Paciente> repositorioPaciente;

    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    private readonly IRepositorio<Funcionario> repositorioFuncionario;

    private readonly IRepositorio<Medicamento> repositorioMedicamento;


    public TelaPrincipal(IRepositorio<Fornecedor> repositorioFornecedor, IRepositorio<Paciente> repositorioPaciente, IRepositorio<Medicamento> repositorioMedicamento, IRepositorio<Funcionario> repositorioFuncionario)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public ITelaOpcoes? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Pacientes");
        Console.WriteLine("3 - Gestão de Medicamentos");
        Console.WriteLine("4 - Gestão de Funcionarios");
        Console.WriteLine("5 - Gestão de Estoque");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaFornecedor(repositorioFornecedor);

        if (opcaoMenuPrincipal == "2")
            return new TelaPaciente(repositorioPaciente);

        if (opcaoMenuPrincipal == "3")
            return new TelaMedicamento(repositorioMedicamento,repositorioFornecedor);

        if (opcaoMenuPrincipal == "4")
            return new TelaFuncionario(repositorioFuncionario);

        if (opcaoMenuPrincipal == "5")
            return null;
            
        return null;
    }
}