namespace Mjml.Net.Types;

public sealed class StringType : IType
{
    private readonly bool isRequired;

    public StringType(bool isRequired)
    {
        this.isRequired = isRequired;
    }

#if NETSTANDARD2_0
    public override bool Validate(string value, ref ValidationContext context)
#else
    public bool Validate(string value, ref ValidationContext context)
#endif
    {
        return !isRequired || !string.IsNullOrEmpty(value);
    }
}
