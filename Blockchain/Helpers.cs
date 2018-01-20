using Blockchain.Algorithms;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Blockchain
{
    /// <summary>
    /// Класс, содержащий методы для увеличения простоты и удобочитаемости кода.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Приведение значения перечисления в удобочитаемый формат.
        /// </summary>
        /// <remarks>
        /// Для корректной работы необходимо использовать атрибут [Description("Name")] для каждого элемента перечисления.
        /// </remarks>
        /// <param name="enumElement"> Элемент перечисления. </param>
        /// <returns> Название элемента. </returns>
        public static string GetDescription(this Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }

        /// <summary>
        /// Проверка корректности хешируемого объекта.
        /// </summary>
        /// <param name="component"> Хешируемый компонент. </param>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        /// <returns> Корректность компонента. true - компонент корректный, false - компонент не корректен. </returns>
        public static bool IsCorrect(this IHashable component, IAlgorithm algorithm = null)
        {
            if(algorithm == null)
            {
                algorithm = AlgorithmHelper.GetDefaultAlgorithm();
            }

            var correct = component.Hash == component.GetHash(algorithm);
            return correct;
        }

        /// <summary>
        /// Проверить псевдоуникальный 128-битный идентификатор на корректность.
        /// </summary>
        /// <param name="guid"> Проверяемый идентификатор. </param>
        /// <returns> Корректность идентификатора. true - идентификатор корректен, false - идентификатор не корректен. </returns>
        public static bool IsCorrect(this Guid guid)
        {
            if(guid == null || guid == Guid.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
