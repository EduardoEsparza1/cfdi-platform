using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record Cantidad
{
    public decimal Value { get; }

    private Cantidad(decimal value)
    {
        Value = value;
    }

    public static Cantidad Create(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "La cantidad debe ser mayor que cero.");
        }

        return new Cantidad(value);
    }
}