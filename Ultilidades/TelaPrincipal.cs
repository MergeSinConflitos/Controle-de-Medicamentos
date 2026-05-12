using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedores;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoqueSaida;

namespace ControleDeMedicamentos.ConsoleApp.Ultilidades;

public class TelaPrincipal
{   
    private readonly IRepositorio<Paciente> repositorioPaciente;

    private readonly IRepositorio<Fornecedor> repositorioFornecedor;

    private readonly IRepositorio<Funcionario> repositorioFuncionario;

    private readonly IRepositorio<Medicamento> repositorioMedicamento;

    private readonly IRepositorio<EstoqueEntrada> repositorioEstoqueEntrada;

    private readonly IRepositorio<EstoqueSaida> repositorioEstoqueSaida;

    public TelaPrincipal(IRepositorio<Fornecedor> repositorioFornecedor, IRepositorio<Paciente> repositorioPaciente, IRepositorio<Medicamento> repositorioMedicamento, IRepositorio<Funcionario> repositorioFuncionario, IRepositorio<EstoqueEntrada> repositorioEstoqueEntrada, IRepositorio<EstoqueSaida> repositorioEstoqueSaida)
    {
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioEstoqueEntrada = repositorioEstoqueEntrada;
        this.repositorioEstoqueSaida = repositorioEstoqueSaida;
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
        Console.WriteLine("5 - Gestão de Estoque Entrada");
        Console.WriteLine("6 - Gestão de Estoque Saida");
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
            return new TelaEstoqueEntrada(repositorioEstoqueEntrada,repositorioMedicamento,repositorioFuncionario);
        
        if (opcaoMenuPrincipal == "6")
            return new TelaEstoqueSaida(repositorioEstoqueSaida,repositorioPaciente,repositorioMedicamento);  
        
        return null;
    }
}