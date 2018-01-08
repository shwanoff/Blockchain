namespace Blockchain.Algorithms
{
    public interface IAlgorithm
    {
        string GetHash(string data);
        string GetHash(IHashable data);
    }
}
