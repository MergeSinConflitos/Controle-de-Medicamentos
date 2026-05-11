using System;
using System.Security.Cryptography;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloDeEstoqueEntrada;

public class EstoqueEntrada
{
    public string Id { get; private set; } = string.Empty;
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int Quantidade { get; set; }

    public EstoqueEntrada(Medicamento medicamento, Funcionario funcionario, int quantidade)
    {
        Id = Convert
             .ToHexString(RandomNumberGenerator.GetBytes(4))
             .ToLower()
             .Substring(0, 7);

        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
    }

    public EstoqueEntrada()
    {

    }

    public void RegistrarEntrada()
    {
        if (Medicamento == null)
        {
            return;
        }
        if (Quantidade <= 0)
        {
            return;
        }
        Medicamento.QuantidadeEmEstoque += Quantidade;
    }

    public List<string> Validar()
    {
        List<string> erros = new List<string>();

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
