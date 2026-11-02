using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSmartHome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TESTE ===\n");
            Console.WriteLine("=== CENTRAL DE CONTROLO SMARTHOME ===\n");
            // Instanciação recorrendo ao Construtor (Bloco 2 e Bloco 5)
            ArCondicionado meuAc = new ArCondicionado("Daikin Premium");
            // Subscrição do Evento de Segurança (Bloco 9)
            meuAc.AlertaLimiteAtingido += NotificarCentralSeguranca;
            // 1. Testar comportamento com aparelho desligado (Encapsulamento bloqueia)
            meuAc.TemperaturaAlvo = 20;
            Console.WriteLine();
            // 2. Ativar o equipamento (Invocação de método - Bloco 4)
            meuAc.Ligar();
            Console.WriteLine();
            // 3. Testar alteração de dados válida (Uso do Set - Bloco 7)
            meuAc.TemperaturaAlvo = 24;
            Console.WriteLine();
            // 4. Testar tentativa de corrupção/erro de dados (Validação rejeita)
            meuAc.TemperaturaAlvo = 12;
            Console.WriteLine();
            // 5. Testar limite crítico para ver o Evento a disparar sozinho
            Console.WriteLine("A configurar para potência máxima de refrigeração...");
            meuAc.TemperaturaAlvo = 16;
            Console.ReadKey();
        }
        // O Manipulador do Evento (Handler - Bloco 9)
        static void NotificarCentralSeguranca(object sender, EventArgs e)
        {
            // Converter o sender de volta para o tipo ArCondicionado para ler os seus dados de forma dinâmica
            ArCondicionado ac = (ArCondicionado)sender;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[NOTIFICAÇÃO CENTRAL]: O Ar Condicionado({ ac.Marca}) atingiu o limite crítico de funcionamento operativo({ ac.TemperaturaAlvo}°C). Monitorizando picos de energia...");
            Console.ResetColor();
        }
    }
}
