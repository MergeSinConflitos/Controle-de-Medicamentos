using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoqueSaida;

public class RepositorioEstoqueSaidaEmMemoria : RepositorioBaseEmMemoria<EstoqueSaida>, IRepositorio<EstoqueSaida>;

