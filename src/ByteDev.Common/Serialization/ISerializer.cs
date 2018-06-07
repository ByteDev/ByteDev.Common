namespace ByteDev.Common.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string input);
    }
}