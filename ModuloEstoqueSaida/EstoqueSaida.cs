using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoqueSaida;

public class EstoqueSaida : EntidadeBase
{
    public DateTime Data { get; set; } 
    public Paciente Paciente { get; set;} 
    public List<Medicamento> Medicamentos { get; set;}
    
    public EstoqueSaida(DateTime data, Paciente paciente, List<Medicamento> medicamentos)
    {   
        Data = data;
        Paciente = paciente;
        Medicamentos = medicamentos;
    }

    public EstoqueSaida()
    {
        
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default)
            erros.Add("O campo 'Data' é obrigatório");

        if (Paciente == null)
            erros.Add("O campo 'Paciente' é obrigatório");

        if (Medicamentos == null || Medicamentos.Count == 0)
            erros.Add("Selecione ao menos um medicamento");

        return erros;
    }
}
