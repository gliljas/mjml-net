namespace Mjml.Net.Types;

public sealed class OneOfType : IType
{
    private readonly List<IType> units;

    public IReadOnlyCollection<IType> Units => units;

    public OneOfType(params IType[] units)
    {
        this.units = [..units];
    }

#if NETSTANDARD2_0
    public override bool Validate(string value, ref ValidationContext context)
#else
    public bool Validate(string value, ref ValidationContext context)
#endif
    {
        foreach (var unit in units)
        {
            if (unit.Validate(value, ref context))
            {
                return true;
            }
        }

        return false;
    }
}
