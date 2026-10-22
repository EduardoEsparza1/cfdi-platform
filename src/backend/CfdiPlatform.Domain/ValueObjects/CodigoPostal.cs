using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record CodigoPostal
{
    private static readonly Regex Formato = new(@"^[0-9]{5}$", RegexOptions.Compiled);

    public string Value { get; }

    private CodigoPostal(string value)
    {
        Value = value;
    }

    public static CodigoPostal Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El código postal no puede estar vacío.",nameof(value));
        }

        var normalized = value.Trim();

        if (!Formato.IsMatch(normalized))
        {
            throw new ArgumentException("El código postal debe contener exactamente 5 dígitos.",nameof(value));
        }

        return new CodigoPostal(normalized);
    }

    public override string ToString() => Value;
}