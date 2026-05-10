using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class RepositorioFuncionarioEmMemoria : RepositorioBaseEmMemoria<Funcionario>, IRepositorio<Funcionario>;
