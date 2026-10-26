using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record ValorUnitario
{
    public decimal Value { get; }

    private ValorUnitario(decimal value)
    {
        Value = value;
    }

    public static ValorUnitario Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "El valor unitario no puede ser negativo.");
        }

        return new ValorUnitario(value);
    }
}