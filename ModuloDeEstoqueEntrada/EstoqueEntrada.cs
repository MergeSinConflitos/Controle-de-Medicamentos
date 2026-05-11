using System;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class EstoqueEntrada
{
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int Quantidade { get; set; }

    public EstoqueEntrada(Medicamento medicamento, Funcionario funcionario, int quantidade, DateTime data = default)
    {
        Data = data;
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
    }

    public List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data < DateTime.Now)
        {
            erros.Add("Informe uma data valida");
        }

        if (Funcionario == null)
        {
            erros.Add("Funcionario invalido");
        }

        if (Medicamento == null)
        {
            erros.Add("Medicamento invalido");
        }

        if (Quantidade < 0)
        {
            erros.Add("Informe um número positivo");
        }

        return erros;
    }
}
