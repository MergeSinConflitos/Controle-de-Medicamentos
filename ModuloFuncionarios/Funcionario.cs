using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class Funcionario : EntidadeBase
{   
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }

    public Funcionario()
    {
    }

    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Funcionario FuncionarioAtualizada = (Funcionario)entidadeAtualizada;

        Nome = FuncionarioAtualizada.Nome;
        Telefone = FuncionarioAtualizada.Telefone;
        Cpf = FuncionarioAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}
