using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.Entities;

public sealed class Receptor
{
    public Guid Id { get; }

    public Rfc Rfc { get; private set; }

    public string Nombre { get; private set; }

    public CodigoPostal DomicilioFiscal { get; private set; }

    public RegimenFiscal RegimenFiscal { get; private set; }

    private Receptor(
        Rfc rfc,
        string nombre,
        CodigoPostal domicilioFiscal,
        RegimenFiscal regimenFiscal
        )
    {
        Id = Guid.NewGuid();

        Rfc = rfc;
        Nombre = nombre;
        DomicilioFiscal = domicilioFiscal;
        RegimenFiscal = regimenFiscal;
       
    }

    public static Receptor Create(
        Rfc rfc,
        string nombre,
        CodigoPostal domicilioFiscal,
        RegimenFiscal regimenFiscal)
    {
        ArgumentNullException.ThrowIfNull(rfc);
        ArgumentNullException.ThrowIfNull(domicilioFiscal);
        ArgumentNullException.ThrowIfNull(regimenFiscal);

        var nombreNormalizado = ValidarNombre(nombre);

        return new Receptor(
            rfc,
            nombreNormalizado,
            domicilioFiscal,
            regimenFiscal);
    }

    public void ActualizarNombre(string nombre)
    {
        var nombreNormalizado = ValidarNombre(nombre);

        Nombre = nombreNormalizado;
    }

    private static string ValidarNombre(string? nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException(
                "El nombre del receptor es obligatorio.",
                nameof(nombre));
        }

        return nombre.Trim();
    }
}
