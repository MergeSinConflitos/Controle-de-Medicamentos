

using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSus { get; set; }
    public string Cpf { get; set; }

    public Paciente()
    {
    }

    public Paciente(string nome, string telefone, string cartaoSus, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Paciente listaAtualizada = (Paciente)entidadeAtualizada;

        Nome = listaAtualizada.Nome;
        Telefone = listaAtualizada.Telefone;
        CartaoSus = listaAtualizada.CartaoSus;
        Cpf = listaAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo '/Nome/' é obrigatório");

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome deve ter entre 3 e 100 caracteres");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo 'Telefone' é obrigatório");
        else if (Telefone.Length != 13 && Telefone.Length != 14)
            erros.Add("O telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX");

        if (string.IsNullOrWhiteSpace(CartaoSus))
            erros.Add("O campo 'Cartão do SUS' é obrigatório");
        else if (CartaoSus.Length != 15)
            erros.Add("O Cartão do SUS deve ter 15 dígitos");

        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo 'CPF' é obrigatório");
        else if (Cpf.Length != 11)
            erros.Add("O CPF deve ter 11 dígitos");

        return erros;

    }
}
