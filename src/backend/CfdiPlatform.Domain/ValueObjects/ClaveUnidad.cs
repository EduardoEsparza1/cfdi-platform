using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record ClaveUnidad
{
    private static readonly Regex ClaveRegex = new(@"\A[A-Z0-9]{1,3}\z", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Value { get; }

    private ClaveUnidad(string value)
    {
        Value = value;
    }

    public static ClaveUnidad Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("La clave de unidad es obligatoria.", nameof(value));
        }

        string clave = value.Trim().ToUpperInvariant();

        if (!ClaveRegex.IsMatch(clave))
        {
            throw new ArgumentException("La clave de unidad debe contener entre 1 y 3 caracteres alfanuméricos.", nameof(value));
        }

        return new ClaveUnidad(clave);
    }
}