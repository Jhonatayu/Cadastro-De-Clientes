using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Gestao_De_Clientes
{
    internal class Program
    {
        [System.Serializable]
        public class Cliente
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string CPF { get; set; } 

        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu {Sair, ListarClientes, AdicionarCliente, RemoverCliente, EditarCliente}

        static void Main(string[] args)
        {

            CarregarClientes();
            ProgramMenu();

            static void ProgramMenu()
            {

                Console.WriteLine("Sistema de Gerenciamento de Clientes - Bem Vindo!\n");
                Console.WriteLine("1- Listar Clientes\n2- Adicionar Cliente\n3- Remover Cliente\n4- Editar Cliente\n0- Sair");
                Console.Write("\nSelecione uma opção: ");
                Menu opcao = (Menu)int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case Menu.ListarClientes:
                        ListarClientes();
                        Console.Write("Pressione ENTER para retornar ao menu. ");
                        ReadLine();
                        Clear();
                        ProgramMenu();
                        break;
                    case Menu.AdicionarCliente:
                        AdicionarCliente();
                        Console.Write("\nPressione ENTER para retornar ao menu. ");
                        ReadLine();
                        Clear();
                        ProgramMenu();
                        break;
                    case Menu.RemoverCliente:
                        RemoverCliente();
                        Console.Write("\nPressione ENTER para retornar ao menu. ");
                        ReadLine();
                        Clear();
                        ProgramMenu();
                        break;
                    case Menu.EditarCliente:
                        EditarCliente();
                        Console.Write("\nPressione ENTER para retornar ao menu. ");
                        ReadLine();
                        Clear();
                        ProgramMenu();
                        break;
                    case Menu.Sair:
                        Clear();
                        Console.WriteLine("Muito obrigado por utilizar nossos serviços!");
                        Console.Write("\nPressione ENTER para fechar o programa. ");
                        ReadLine();
                        break;
                    default:
                        Clear();
                        Console.WriteLine("Por favor digite uma opção válida. ");
                        Console.Write("\nPressione ENTER para retornar ao menu. ");
                        ReadLine();
                        Clear();
                        ProgramMenu();
                        break;

                }
            }   
        }

        static void AdicionarCliente()
        {
            Clear();
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente");

            Console.Write("\nDigite o NOME do cliente: ");
            cliente.Nome = Console.ReadLine();

            Console.Write("Digite o EMAIL do cliente: ");
            cliente.Email = Console.ReadLine();

            Console.Write("Digite o TELEFONE do cliente: ");
            cliente.Telefone = Console.ReadLine();

            Console.Write("Digite o CPF do cliente: ");
            cliente.CPF = Console.ReadLine();

            Console.Write("\nDeseja alterar os dados? (sim/não) ");
            string alterar = Console.ReadLine().ToUpper();

            if (alterar == "SIM")
            {
                AdicionarCliente();

            }else if (alterar == "NÃO" || alterar == "NAO")
            {
                clientes.Add(cliente);
                SalvarCliente();
                Clear();
                Console.WriteLine("Cliente adicionado com sucesso! ");
            }
        }

        static void ListarClientes()
        {
            if (clientes.Count > 0)
            {
                Clear();
                Console.WriteLine("Lista de Clientes");
                ListarClientesLoop();

            }else
            {
                Clear();
                Console.WriteLine("Nenhum cliente cadastrado, por favor cadastre algum para poder lista-lo.\n");
            }

        }

        static void ListarClientesLoop()
        {
            if (clientes.Count == 1)
            {
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"\nID: {i}");
                    Console.WriteLine($"Nome: {cliente.Nome}");
                    Console.WriteLine($"Email: {cliente.Email}");
                    Console.WriteLine($"Telefone: {cliente.Telefone}");
                    Console.WriteLine($"CPF: {cliente.CPF}\n");
                    i++;
                }

            }else if (clientes.Count >= 2)
            {
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"\nID: {i}");
                    Console.WriteLine($"Nome: {cliente.Nome}");
                    Console.WriteLine($"Email: {cliente.Email}");
                    Console.WriteLine($"Telefone: {cliente.Telefone}");
                    Console.WriteLine($"CPF: {cliente.CPF}");
                    Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - -\n");
                    i++;
                }
            }

        }

        static void SalvarCliente()
        {
            string json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("clients.json", json);
        }

        static void CarregarClientes()
        {
            if (File.Exists("clients.json"))
            {
                var json = File.ReadAllText("clients.json");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    clientes = JsonSerializer.Deserialize<List<Cliente>>(json);
                }
            }
        }

        static void RemoverCliente()
        {
            if (clientes.Count > 0)
            {
                Clear();
                ListarClientesLoop();
                Console.Write("Digite o ID do cliente que deseja remover: ");
                int id = int.Parse(Console.ReadLine());
                if (id >= 0 && id < clientes.Count)
                {
                    clientes.RemoveAt(id);
                    SalvarCliente();
                    Clear();
                    Console.WriteLine("Cliente removido com sucesso! ");

                }
                else
                {
                    Clear();
                    Console.WriteLine("ID inválido, por favor digite um ID válido. ");
                    Console.Write("\nPressione ENTER para retornar. ");
                    ReadLine();
                    RemoverCliente();
                }
            }
        }

        static void EditarCliente()
        {
            if (clientes.Count > 0)
            {
                Clear();
                ListarClientesLoop();
                Console.Write("Digite o ID do cliente que deseja alterar: ");
                int id = int.Parse(Console.ReadLine());

                if (id >= 0 && id < clientes.Count)
                {
                    Clear();
                    Console.WriteLine($"Nome do Cliente: {clientes[id].Nome}");
                    Console.WriteLine($"Email do Cliente: {clientes[id].Email}");
                    Console.WriteLine($"Telefone do Cliente: {clientes[id].Telefone}");
                    Console.WriteLine($"CPF do Cliente: {clientes[id].CPF}\n");

                    Console.Write("Digite o NOME que irá substituir: ");
                    clientes[id].Nome = Console.ReadLine();

                    Console.Write("Digite o EMAIL que irá substituir: ");
                    clientes[id].Email = Console.ReadLine();

                    Console.Write("Digite o TELEFONE que irá substituir: ");
                    clientes[id].Telefone = Console.ReadLine();

                    Console.Write("Digite o CPF que irá substituir: ");
                    clientes[id].CPF = Console.ReadLine();

                    SalvarCliente();
                    Clear();
                    Console.WriteLine("Cliente alterado com sucesso! ");

                }else
                {
                    Clear();
                    Console.WriteLine("ID inválido, por favor digite um ID válido. ");
                    Console.Write("\nPressione ENTER para retornar. ");
                    ReadLine();
                    EditarCliente();
                }
            }
        }

        static void ReadLine()
        {
            Console.ReadLine();
        }
        static void Clear()
        {
            Console.Clear();
        }
    }
}
