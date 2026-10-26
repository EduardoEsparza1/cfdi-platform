using System;
using System.Collections.Generic;
using System.Text;
using CfdiPlatform.Domain.ValueObjects;

namespace CfdiPlatform.Domain.Entities;

public sealed class Concepto
{
    public Guid Id { get; }

    public ClaveProdServ ClaveProdServ { get; }

    public Cantidad Cantidad { get; }

    public ClaveUnidad ClaveUnidad { get; }

    public string Descripcion { get; }

    public ValorUnitario ValorUnitario { get; }

    public Importe Importe { get; }

    public Descuento? Descuento { get; }

    public ObjetoImp ObjetoImp { get; }

    private Concepto(ClaveProdServ claveProdServ, Cantidad cantidad, ClaveUnidad claveUnidad, string descripcion, ValorUnitario valorUnitario, Importe importe, ObjetoImp objetoImp, Descuento? descuento)
    {
        Id = Guid.NewGuid();
        ClaveProdServ = claveProdServ;
        Cantidad = cantidad;
        ClaveUnidad = claveUnidad;
        Descripcion = descripcion;
        ValorUnitario = valorUnitario;
        Importe = importe;
        ObjetoImp = objetoImp;
        Descuento = descuento;
    }

    public static Concepto Create(ClaveProdServ claveProdServ, Cantidad cantidad, ClaveUnidad claveUnidad, string descripcion, ValorUnitario valorUnitario, ObjetoImp objetoImp, decimal? descuento = null)
    {
        ArgumentNullException.ThrowIfNull(claveProdServ);
        ArgumentNullException.ThrowIfNull(cantidad);
        ArgumentNullException.ThrowIfNull(claveUnidad);
        ArgumentNullException.ThrowIfNull(valorUnitario);
        ArgumentNullException.ThrowIfNull(objetoImp);

        if (string.IsNullOrWhiteSpace(descripcion))
        {
            throw new ArgumentException("La descripción del concepto es obligatoria.", nameof(descripcion));
        }

        Importe importe = Importe.Calculate(cantidad, valorUnitario);
        Descuento? descuentoCalculado = descuento.HasValue ? Descuento.Create(descuento.Value, importe) : null;

        return new Concepto(claveProdServ, cantidad, claveUnidad, descripcion.Trim(), valorUnitario, importe, objetoImp, descuentoCalculado);
    }
}
