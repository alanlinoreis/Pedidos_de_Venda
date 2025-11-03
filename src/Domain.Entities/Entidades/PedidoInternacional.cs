using System;

namespace Domain.Entities.Entidades
{
    public sealed class PedidoInternacional : Pedido
    {
        public PedidoInternacional(
            Func<decimal, decimal>? frete = null,
            Func<decimal, decimal>? promocao = null)
            : base(frete, promocao)
        {
        }

        protected override void Validar()
        {
            Console.WriteLine("[Internacional] Validando dados de exportação...");
        }

        protected override decimal CalcularSubtotal()
        {
            Console.WriteLine("[Internacional] Calculando subtotal com câmbio e taxas de importação...");
            return 500m;
        }

        protected override string EmitirRecibo(decimal total)
            => $"Commercial Invoice emitida no valor de {total:C}";
    }
}
