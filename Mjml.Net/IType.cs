namespace Mjml.Net;

#if NETSTANDARD2_0
public abstract class IType
{
    public abstract bool Validate(string value, ref ValidationContext context);

    public virtual string Coerce(string value)
    {
        return value;
    }
}
#else
public interface IType
{
    bool Validate(string value, ref ValidationContext context);

    public string Coerce(string value)
    {
        return value;
    }
}
#endif
