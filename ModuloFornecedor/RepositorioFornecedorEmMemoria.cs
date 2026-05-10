using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Memoria;

namespace ControleDeMedicamentos.ConsoleApp;

public class RepositorioFornecedorEmMemoria : RepositorioBaseEmMemoria<Fornecedor>, IRepositorio<Fornecedor>;
