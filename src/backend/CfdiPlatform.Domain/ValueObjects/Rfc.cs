using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record Rfc
{
    public string Value { get; }

    private Rfc(string value)
    {
        Value = value;
    }

    public static Rfc Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El RFC no puede estar vacío.",nameof(value));

        var normalized = value.Trim().ToUpperInvariant();

        const string pattern = @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$";

        if (!Regex.IsMatch(normalized, pattern))
            throw new ArgumentException("El RFC tiene un formato inválido.",nameof(value));

        return new Rfc(normalized);
    }

    public override string ToString() => Value;
}