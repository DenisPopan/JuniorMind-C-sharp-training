namespace Json
{
    public interface IPattern
    {
        IMatch Match(string text);
    }
}
