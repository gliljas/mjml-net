﻿using System.Globalization;

namespace Mjml.Net.Components
{
    public static class UnitParser
    {
        public static (double Value, UnitType Unit) Parse(string? rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return (0, UnitType.Unknown);
            }

            var span = rawValue.AsSpan().Trim();

            var i = 0;
            for (i = 0; i < span.Length; i++)
            {
                var c = span[i];

                if (!char.IsNumber(c) && c != '.' && c != ',')
                {
                    break;
                }
            }

            var unitSpan = span[i..].Trim();
            var unitType = UnitType.Unknown;

            var f = new string(unitSpan);

            if (unitSpan.SequenceEqual("px"))
            {
                unitType = UnitType.Pixels;
            }
            else if (unitSpan.SequenceEqual("%"))
            {
                unitType = UnitType.Percent;
            }

            var valueSpan = span[..i];

            if (valueSpan.Length == 0)
            {
                return (0, unitType);
            }

            double.TryParse(valueSpan, NumberStyles.Any, CultureInfo.InvariantCulture, out var temp);

            return (temp, unitType);
        }
    }
}
