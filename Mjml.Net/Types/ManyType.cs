namespace Mjml.Net.Types;

public sealed class ManyType : IType
{
    private readonly IType unit;
    private readonly int min;
    private readonly int max;

    public IType Unit => unit;

    public int Min => min;

    public int Max => max;

    public ManyType(IType unit, int min, int max)
    {
        this.unit = unit;
        this.min = min;
        this.max = max;
    }

#if NETSTANDARD2_0
    public override bool Validate(string value, ref ValidationContext context)
#else
    public bool Validate(string value, ref ValidationContext context)
#endif
    {
        var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < min || parts.Length > max)
        {
            return false;
        }

        foreach (var part in parts)
        {
            if (!unit.Validate(part, ref context))
            {
                return false;
            }
        }

        return true;
    }
}
