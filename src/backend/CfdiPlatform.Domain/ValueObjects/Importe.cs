using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record Importe
{
    public decimal Value { get; }

    private Importe(decimal value)
    {
        Value = value;
    }

    public static Importe Calculate(Cantidad cantidad, ValorUnitario valorUnitario)
    {
        ArgumentNullException.ThrowIfNull(cantidad);
        ArgumentNullException.ThrowIfNull(valorUnitario);

        decimal importe = checked(cantidad.Value * valorUnitario.Value);

        return new Importe(importe);
    }
}