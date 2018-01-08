namespace Blockchain.Algorithms
{
    public static class AlgorithmHelper
    {
        public static string GetHash(this IHashable component, IAlgorithm algorithm)
        {
            var hash = algorithm.GetHash(component);
            return hash;
        }
    }
}
