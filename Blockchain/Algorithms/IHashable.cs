#define CONTRACTS_FULL

using System.Diagnostics.Contracts;

namespace Blockchain.Algorithms
{
    /// <summary>
    /// Интерфейс для объектов которые могут быть хешированы.
    /// </summary>
    [ContractClass(typeof(IHashableContract))]
    public interface IHashable
    {
        /// <summary>
        /// Хранимый хеш компонента.
        /// </summary>
        string Hash { get; }

        /// <summary>
        /// Получить данные из объекта, на основе которых будет строиться хеш.
        /// </summary>
        /// <returns> Хешируемые данные. </returns>
        string GetStringForHash();
    }

    /// <summary>
    /// Класс, определяющий контракты для интерфейса.
    /// </summary>
    [ContractClassFor(typeof(IHashable))]
    internal abstract class IHashableContract : IHashable
    {
        string IHashable.Hash
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return string.Empty;
            }
        }

        string IHashable.GetStringForHash()
        {
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
            return string.Empty;
        }
    }
}
