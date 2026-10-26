using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record UsoCfdi
{
    public string Value { get; }

    private UsoCfdi(string value)
    {
        Value = value;
    }

    public static UsoCfdi Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El uso del CFDI es obligatorio.", nameof(value));
        }

        var normalized = value.Trim().ToUpperInvariant();

        bool formatoValido =
            (normalized.Length == 3 &&
             char.IsLetter(normalized[0]) &&
             char.IsDigit(normalized[1]) &&
             char.IsDigit(normalized[2]))
            ||
            normalized == "CP01";

        if (!formatoValido)
        {
            throw new ArgumentException("El formato del uso del CFDI no es válido.", nameof(value));
        }

        return new UsoCfdi(normalized);
    }

    public override string ToString() => Value;
}