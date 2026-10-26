using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record ClaveProdServ
{
    private static readonly Regex ClaveRegex = new(@"^[0-9]{8}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Value { get; }

    private ClaveProdServ(string value)
    {
        Value = value;
    }

    public static ClaveProdServ Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("La clave de producto o servicio es obligatoria.", nameof(value));
        }

        string clave = value.Trim();

        if (!ClaveRegex.IsMatch(clave))
        {
            throw new ArgumentException("La clave de producto o servicio debe contener exactamente ocho dígitos.", nameof(value));
        }

        return new ClaveProdServ(clave);
    }
}
