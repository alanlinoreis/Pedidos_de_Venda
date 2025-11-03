using System;

namespace Domain.Entities.Entidades
{
    public sealed class PedidoNacional : Pedido
    {
        public PedidoNacional(
            Func<decimal, decimal>? frete = null,
            Func<decimal, decimal>? promocao = null)
            : base(frete, promocao)
        {
        }

        protected override void Validar()
        {
            Console.WriteLine("[Nacional] Validando CNPJ e dados fiscais...");
        }

        protected override decimal CalcularSubtotal()
        {
            Console.WriteLine("[Nacional] Calculando subtotal com impostos locais (ICMS/IPI)...");
            return 200m;
        }

        protected override string EmitirRecibo(decimal total)
            => $"NF-e emitida no valor de {total:C}";
    }
}
