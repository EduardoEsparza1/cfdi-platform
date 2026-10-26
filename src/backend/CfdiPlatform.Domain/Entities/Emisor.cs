using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.Entities;

public sealed class Emisor
{
    public Guid Id { get; }

    public Rfc Rfc { get; }

    public string Nombre { get; private set; }

    public RegimenFiscal RegimenFiscal { get; private set; }

    public CodigoPostal CodigoPostal { get; private set; }

    private Emisor(
        Guid id,
        Rfc rfc,
        string nombre,
        RegimenFiscal regimenFiscal,
        CodigoPostal codigoPostal)
    {
        Id = id;
        Rfc = rfc;
        Nombre = nombre;
        RegimenFiscal = regimenFiscal;
        CodigoPostal = codigoPostal;
    }

    public static Emisor Create(
        Rfc rfc,
        string nombre,
        RegimenFiscal regimenFiscal,
        CodigoPostal codigoPostal)
    {
        ArgumentNullException.ThrowIfNull(rfc);
        ArgumentNullException.ThrowIfNull(regimenFiscal);
        ArgumentNullException.ThrowIfNull(codigoPostal);

        return new Emisor(
            Guid.NewGuid(),
            rfc,
            ValidarNombre(nombre),
            regimenFiscal,
            codigoPostal);
    }

    public void ActualizarNombre(string nombre)
    {
        Nombre = ValidarNombre(nombre);
    }

    public void ActualizarRegimenFiscal(RegimenFiscal regimenFiscal)
    {
        ArgumentNullException.ThrowIfNull(regimenFiscal);
        RegimenFiscal = regimenFiscal;
    }

    public void ActualizarCodigoPostal(CodigoPostal codigoPostal)
    {
        ArgumentNullException.ThrowIfNull(codigoPostal);
        CodigoPostal = codigoPostal;
    }

    private static string ValidarNombre(string? nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre o razón social es obligatorio.",nameof(nombre));
        }

        return nombre.Trim();
    }
}