using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record Descuento
{
    public decimal Value { get; }

    private Descuento(decimal value)
    {
        Value = value;
    }

    public static Descuento Create(decimal value, Importe importe)
    {
        ArgumentNullException.ThrowIfNull(importe);

        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El descuento no puede ser negativo.");
        }

        if (value > importe.Value)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El descuento no puede superar el importe del concepto.");
        }

        return new Descuento(value);
    }
}