namespace Mjml.Net.Types;

public sealed class NumberType : IType
{
    private readonly Unit[] units;

    public IReadOnlyCollection<Unit> Units => units;

    public NumberType(params Unit[] units)
    {
        this.units = units;
    }

#if NETSTANDARD2_0
    public override bool Validate(string value, ref ValidationContext context)
#else
    public bool Validate(string value, ref ValidationContext context)
#endif
    {
        var trimmed = value.AsSpan().Trim();

        if (trimmed.Length == 1 && trimmed[0] == '0')
        {
            return true;
        }

        var (_, unit) = UnitParser.Parse(value);

        return units.Contains(unit);
    }
}
