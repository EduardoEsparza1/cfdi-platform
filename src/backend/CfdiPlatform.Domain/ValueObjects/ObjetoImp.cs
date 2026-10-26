using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects;

public sealed record ObjetoImp
{
    public string Value { get; }

    private ObjetoImp(string value)
    {
        Value = value;
    }

    public static ObjetoImp Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El objeto de impuesto es obligatorio.", nameof(value));
        }

        string clave = value.Trim();

        if (clave is not ("01" or "02" or "03" or "04" or "05"))
        {
            throw new ArgumentException("La clave de objeto de impuesto no es válida.", nameof(value));
        }

        return new ObjetoImp(clave);
    }
}