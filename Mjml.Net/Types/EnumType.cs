namespace Mjml.Net.Types;

public class EnumType : IType
{
    private readonly HashSet<string> allowedValues;
    private readonly bool isOptional;

    public bool IsOptional => isOptional;

#if NETSTANDARD2_0
    public IReadOnlyCollection<string> AllowedValues => allowedValues;
#else
    public IReadOnlySet<string> AllowedValues => allowedValues;
#endif
    public EnumType(bool isOptional, params string[] values)
    {
        allowedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);

        this.isOptional = isOptional;
    }

#if NETSTANDARD2_0
    public override bool Validate(string value, ref ValidationContext context)
#else
    public bool Validate(string value, ref ValidationContext context)
#endif
    {
        if (string.IsNullOrWhiteSpace(value) && isOptional)
        {
            return true;
        }

        return allowedValues.Contains(value);
    }
}
