using ContaBancaria.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Entities
{
    class ContaCorrente
    {
        private TipoConta TipoConta { get; set; }
        public string NomeCorrentista { get; private set; }
        private int Agencia { get; set; }
        public int NumeroConta { get; private set; }
        private double LimiteChequeEspecialConcedido { get; set; }
        public double LimiteChequeEspecial { get; set; }
        private double Saldo { get; set; }
        private double SaldoTotal { get; set; }


        /// <summary>
        /// Metodo Contrutor principal da classe
        /// </summary>
        /// <param name="tipoConta"></param>
        /// <param name="nomeCorrentista"></param>
        /// <param name="agencia"></param>
        /// <param name="numeroConta"></param>
        /// <param name="limiteCredito"></param>
        /// <param name="saldoAtual"></param>
        public ContaCorrente(TipoConta tipoConta, string nomeCorrentista, int agencia, int numeroConta, double limiteCredito, double saldoAtual)
        {
            TipoConta = tipoConta;
            NomeCorrentista = nomeCorrentista;
            Agencia = agencia;
            NumeroConta = numeroConta;
            LimiteChequeEspecialConcedido = limiteCredito;
            LimiteChequeEspecial = limiteCredito;
            Saldo = saldoAtual;
            SaldoTotal = saldoAtual + limiteCredito;
        }

        /// <summary>
        /// Metodo que efetuado deposito na conta do correntista instanciado
        /// </summary>
        /// <param name="valorDeposito"></param>
        public void EfetuarDeposito(double valorDeposito)
        {

            //Atualiza Limite de Cheque Especial e saldo
            if (LimiteChequeEspecial < LimiteChequeEspecialConcedido)
            {
                var valorAddLimiteChequeEspecial = LimiteChequeEspecialConcedido - LimiteChequeEspecial;
                LimiteChequeEspecial += valorAddLimiteChequeEspecial;
                valorDeposito -= valorAddLimiteChequeEspecial;
                if (valorDeposito == 0)
                {
                    Saldo = 0;
                }
                else
                {
                    Saldo += valorDeposito;
                }
            }
            else
            {
                ////Atualiza somente saldo 
                Saldo += valorDeposito;
            }

            //Atualiza saldo total (Saldo  + Limite de credito)
            SaldoTotal = Saldo + LimiteChequeEspecial;

            Console.WriteLine();
            Console.WriteLine($"Saldo atual da conta do correntista {NomeCorrentista} é R$ {Saldo} com Cheque especial saldo total R${SaldoTotal} ");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Pressione <ENTER> tecla para continuar ...");
            Console.ReadLine();
        }

        /// <summary>
        /// Metodo que efetua o saque na conta do corretista instanciado 
        /// </summary>
        /// <param name="valorSaque"></param>
        /// <returns></returns>
        public bool EfetuarSaque(double valorSaque)

        {
            var limiteChequeEspecial = LimiteChequeEspecial;

            var saldoFinal = (Saldo + LimiteChequeEspecial) - valorSaque;

            // Validação de Saldo para saque
            if (saldoFinal < 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Não há Saldo suficiente para transação Sr(a): {NomeCorrentista}!");
                Console.ReadLine();
                return false;
            }
            else
            {
                if (Saldo >= valorSaque)
                {
                    Saldo -= valorSaque;
                }
                else

                {
                    // Verifica se o Saldo total é menor que o saque para efetuar utilizando o cheque especial
                    if (SaldoTotal >= valorSaque)
                    {
                        //Atualiza saldo
                        Saldo -= valorSaque;
                        LimiteChequeEspecial -= -Saldo;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Não há Saldo suficiente para transação Sr(a): {NomeCorrentista}!");
                        Console.ReadLine();
                        return false;
                    }

                }

            }

            //Atualiza saldo total (Saldo  + Limite de credito)
            SaldoTotal = Saldo + limiteChequeEspecial;

            Console.WriteLine();

            Console.Write($"Saldo atual da conta de correntista {NomeCorrentista} é de R$: {Saldo}, Saldo + Limite ChequeEspecial é de R$: {SaldoTotal}");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Pressione <ENTER> tecla para continuar ...");
            Console.ReadLine();
            return true;
        }

        /// <summary>
        /// Metodo que efetuar transferencia na conta do corretista instancia para conta indicada
        /// </summary>
        /// <param name="valorTransferencia"></param>
        /// <param name="contaDestino"></param>
        public void EfetuarTransferencia(double valorTransferencia, ContaCorrente contaDestino)
        {
            {
                if (EfetuarSaque(valorTransferencia))
                {
                    contaDestino.EfetuarDeposito(valorTransferencia);
                }
            }
        }

        public override string ToString()
        {
            var contaCorrente = new StringBuilder();
            contaCorrente.AppendLine();
            contaCorrente.Append("Agência: " + Agencia + " | ");
            contaCorrente.Append("Número da Conta: " + NumeroConta + " | ");
            contaCorrente.AppendLine("Tipo Conta: " + TipoConta);
            contaCorrente.AppendLine("Nome Correntista: " + NomeCorrentista);
            contaCorrente.AppendLine("Saldo Atual: " + Saldo);
            contaCorrente.AppendLine("Limite Credito: " + LimiteChequeEspecial);
            contaCorrente.AppendLine("Saldo + Limite Credito: " + SaldoTotal);
            return contaCorrente.ToString();
        }

    }
}
