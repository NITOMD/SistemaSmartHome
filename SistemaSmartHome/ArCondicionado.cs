using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSmartHome
{
    internal class ArCondicionado
    {
        // 1. DECLARAÇÃO DE EVENTO (Bloco 9)
        public event EventHandler AlertaLimiteAtingido;

        // 2. ATRIBUTO PRIVADO (Bloco 6 - Proteção de Estado)
        private int _temperaturaAlvo;

        // 3. PROPRIEDADES AUTOMÁTICAS (Bloco 7 - Abstração de Dados simples)
        public string Marca { get; set; }
        public bool IsLigado { get; private set; } // O exterior pode ler, mas só a classe pode alterar

        // 4. PROPRIEDADE COMPLETA COM ENCAPSULAMENTO (Bloco 7 - Validação de Dados)
        public int TemperaturaAlvo
        {
            get { return _temperaturaAlvo; }
            set
            {
                if (!IsLigado)
                {
                    Console.WriteLine($"[Aviso] Não é possível alterar a temperatura.O Ar Condicionado { Marca} está DESLIGADO.");
                    return;
                }
                // Filtro de Segurança do Dispositivo: Limites Operacionais entre 16°C e 30°C
            
                if (value >= 16 && value <= 30)
                {
                    _temperaturaAlvo = value;
                    Console.WriteLine($"[Equipamento] Temperatura ajustada com sucesso para: { _temperaturaAlvo}°C");
                    // Se atingir os limites críticos do aparelho, dispara o evento autónomo
                    if (_temperaturaAlvo == 16 || _temperaturaAlvo == 30)
                    {
                        // Disparar Evento (Bloco 9)
                        AlertaLimiteAtingido?.Invoke(this,
                        EventArgs.Empty);
                    }
                }
                else
                {
                    Console.WriteLine($"[ERRO] Temperatura de { value}°C está fora dos limites de segurança(16°C - 30°C)!");              
                }
            }
        }
        // 5. CONSTRUTOR PARAMETRIZADO (Bloco 5 - Nascimento Seguro)
        public ArCondicionado(string marca)
        {
            this.Marca = marca;
            this.IsLigado = false; // Nasce desligado por segurança
            this._temperaturaAlvo = 22; // Temperatura padrão inicial de fábrica
        }
        // 6. MÉTODOS E COMPORTAMENTOS (Bloco 4 - Ações do Objeto)
        public void Ligar()
        {
            if (!IsLigado)
            {
                IsLigado = true;
                Console.WriteLine($"[Equipamento] O Ar Condicionado { Marca} foi LIGADO. Modo Eco Ativo(22°C).");
            }
        }
        public void Desligar()
        {
            if (IsLigado)
            {
                IsLigado = false;
                Console.WriteLine($"[Equipamento] O Ar Condicionado { Marca} foi DESLIGADO.");
            }
        }
    }
}
