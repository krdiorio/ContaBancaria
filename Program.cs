using ContaBancaria.Enum;
using ContaBancaria.Entities;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using ContaBancaria.Entities.Exceptions;

namespace ContaBancaria
{
    class Program
    {
        static List<ContaCorrente> listaContasCorrentes = new List<ContaCorrente>();

        static void Main(string[] args)
        {
            var opcaoSelecionada = MenuPrincipal();

            while (opcaoSelecionada.ToUpper() != "S")
            {
                switch (opcaoSelecionada)
                {
                    case "1":
                        CadastrarCorrentista();
                        break;
                    case "2":
                        EfetuarDeposito();
                        break;
                    case "3":
                        EfetuarSaque();
                        break;
                    case "4":
                        EfetuarTransferencia();
                        break;
                    case "5":
                        ListarContas();
                        break;
                    case "6":
                        ExcluirConta();
                        break;
                    case "L":
                        LimparTela();
                        break;
                    default:
                        throw new DomainException("Opção inválida ...");
                }

                opcaoSelecionada = MenuPrincipal();
            }
            Console.WriteLine();
            Console.WriteLine("Logof executado com sucesso !!!");
            Console.ReadLine();
        }

        /// <summary>
        /// Cadastro nova conta corrente
        /// </summary>
        private static void CadastrarCorrentista()
        {
            LimparTela();

            Console.WriteLine("Cadastrar nova Conta corrente");
            Console.WriteLine();

            Console.Write("Digite (1) Pessoa Fisica - (2) Pessoa Juridica: ");
            var tipoConta = int.Parse(Console.ReadLine());

            if (tipoConta == 1 || tipoConta == 2)
            {
                Console.Write("Digite o Nome do Cliente: ");
                var nomeCorrentista = Console.ReadLine();

                Console.Write("Digite o Saldo inicial: ");
                var saldoIncial = double.Parse(Console.ReadLine());

                Console.Write("Digite o Limete crédito especial: ");
                var limiteCredito = double.Parse(Console.ReadLine());

                // Gerar radomicamente numero da agencia e conta corrente
                var numAleatorio = new Random();
                var agenciaConta = numAleatorio.Next(1, 5);
                var contaCorrente = numAleatorio.Next(1, 21);
                Console.WriteLine($"Número da Agencia: {agenciaConta}");
                Console.WriteLine($"Número da Conta corrente: {contaCorrente}");

                //Instancia nova conta corrente
                var novaConta = new ContaCorrente((TipoConta)tipoConta, nomeCorrentista, agenciaConta, contaCorrente, limiteCredito, saldoIncial);

                listaContasCorrentes.Add(novaConta);
                Console.WriteLine();
                Console.Write("Conta cadastra com sucesso, pressione <ENTER>para voltar ao menu ...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                Console.Write("Opção inválida, pressione <ENTER>para voltar ao menu ...");
                Console.ReadLine();
                return;
            }

        }

        /// <summary>
        /// Excluir conta corrente existente
        /// </summary>
        private static void ExcluirConta()
        {
            ListarContas();

            if (listaContasCorrentes.Count() > 0)
            {
                ListarContas();
                Console.WriteLine();
                Console.Write("Digite o ID da conta para exclusão: ");
                var idConta = int.Parse(Console.ReadLine());

                for (int i = 0; i < listaContasCorrentes.Count; i++)
                {
                    if (i == idConta)
                    {
                        Console.Write("Confirma Exclusão (S - SIM / N - NÃO): ");
                        var respostaExclusao = Console.ReadLine().ToUpper();
                        if (respostaExclusao == "S" || respostaExclusao == "s")
                        {
                            ContaCorrente conta = listaContasCorrentes[i];
                            listaContasCorrentes.Remove(conta);
                            Console.WriteLine();
                            Console.WriteLine("Conta excluida com sucesso !!!");

                        }
                        else if (respostaExclusao == "N" || respostaExclusao == "n")
                        {
                            return;
                        }

                    }
                }
            }
            else
            {
                return;
            }

            //}

            Console.WriteLine();
            Console.Write("Pressione <ENTER> tecla para voltar ao menu ...");
            Console.ReadLine();



        }

        /// <summary>
        /// Efetua deposito em conta corrente já cadatrada
        /// </summary>
        private static void EfetuarDeposito()
        {
            ListarContas();

            if (listaContasCorrentes.Count() > 0)
            {
                Console.WriteLine();
                Console.Write("Digite o Id da conta: ");
                int indiceConta = int.Parse(Console.ReadLine());

                Console.Write("Digite o valor a ser depositado: ");
                double valorDeposito = double.Parse(Console.ReadLine());

                listaContasCorrentes[indiceConta].EfetuarDeposito(valorDeposito);
            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// Efetua saque em conta corrente já cadastrada
        /// </summary>
        private static void EfetuarSaque()
        {
            ListarContas();
            if (listaContasCorrentes.Count() > 0)
            {
                Console.WriteLine();
                Console.Write("Digite o Id da conta corrente: ");
                int indiceConta = int.Parse(Console.ReadLine());

                Console.Write("Digite o valor do saque: ");
                double valorSaque = double.Parse(Console.ReadLine());

                listaContasCorrentes[indiceConta].EfetuarSaque(valorSaque);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Efetuar transfenrecia entre conta correntes já cadatradas
        /// </summary>
        private static void EfetuarTransferencia()
        {
            ListarContas();

            if (listaContasCorrentes.Count() > 0)
            {
                Console.Write("Digite o Id da conta de origem: ");
                int indiceContaOrigem = int.Parse(Console.ReadLine());

                Console.Write("Digite o Id da conta de destino: ");
                int indiceContaDestino = int.Parse(Console.ReadLine());

                Console.Write("Digite o valor a ser transferido: ");
                double valorTransferencia = double.Parse(Console.ReadLine());

                listaContasCorrentes[indiceContaOrigem].EfetuarTransferencia(valorTransferencia, listaContasCorrentes[indiceContaDestino]);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// List contas correntes cadastradas
        /// </summary>
        private static void ListarContas()
        {
            LimparTela();
            Console.WriteLine("Relatorio de Correntista");
            Console.WriteLine();
            if (listaContasCorrentes.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                Console.WriteLine();
                Console.WriteLine("Pressione <ENTER> tecla para voltar ao menu ...");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < listaContasCorrentes.Count; i++)
            {
                ContaCorrente conta = listaContasCorrentes[i];
                Console.Write($"ID: #{i}");
                Console.WriteLine(conta);
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

            }

            Console.WriteLine();
            Console.Write("Pressione <ENTER> para continuar ...");
            Console.ReadLine();
        }

        /// <summary>
        /// Limpa tela
        /// </summary>
        private static void LimparTela()
        {
            Console.Clear();
        }

        /// <summary>
        /// Menu principal das operações disponiveis
        /// </summary>
        /// <returns></returns>
        private static string MenuPrincipal()
        {
            LimparTela();
            Console.WriteLine();
            Console.WriteLine("krdiorio Seu Banco 100% Digital");
            Console.WriteLine();
            Console.WriteLine("**  Menu Principal **");
            Console.WriteLine();
            Console.WriteLine("1- Cadastrar nova conta");
            Console.WriteLine("2- Efetuar Deposito");
            Console.WriteLine("3- Efetuar Saque");
            Console.WriteLine("4- Efetuar Transferencia");
            Console.WriteLine("5- Relatorio de correntista");
            Console.WriteLine("6- Excluir de correntista");
            Console.WriteLine("S- Sair");
            Console.WriteLine();
            Console.Write("Digite uma opção: ");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }

    }
}

