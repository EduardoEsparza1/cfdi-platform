using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record RegimenFiscal
{
    public string Value { get; }

    private RegimenFiscal(string value)
    {
        Value = value;
    }

    public static RegimenFiscal Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El régimen fiscal es obligatorio.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length != 3 || !normalized.All(c => c >= '0' && c <= '9'))
        {
            throw new ArgumentException("El régimen fiscal debe contener exactamente 3 dígitos.", nameof(value));
        }

        return new RegimenFiscal(normalized);
    }

    public override string ToString() => Value;
}