using System;
using System.Collections.Generic;
using System.Text;

namespace CfdiPlatform.Domain.ValueObjects
{
    public sealed class Subtotal
    {
        public decimal Value { get; }

        public Subtotal(decimal value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value),"El subtotal no puede ser negativo.");

            Value = value;
        }
    }
}
