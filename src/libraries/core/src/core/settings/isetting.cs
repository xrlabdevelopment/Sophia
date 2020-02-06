namespace Sophia.Core
{
    public interface ISetting
    {
        string Key { get; }
        string Value { get; set; }
    }
}
