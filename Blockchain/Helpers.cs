using Blockchain.Algorithms;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;

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
        /// Получение данных в формате JSON из хешируемого компонента.
        /// </summary>
        /// <param name="component"> Хешируемый компонент. </param>
        /// <returns> Данные компонента в формате JSON. </returns>
        public static string GetJson(this IHashable component)
        {
            var jsonFormatter = new DataContractJsonSerializer(component.GetType());

            using (var ms = new MemoryStream())
            {
                jsonFormatter.WriteObject(ms, component);
                var jsonString = Encoding.UTF8.GetString((ms.ToArray()));
                return jsonString;
            }
        }

        /// <summary>
        /// Десериализовать хешируемый компонент из JSON данных.
        /// </summary>
        /// <param name="type"> Тип хешируемого компонента. </param>
        /// <param name="json"> Строка с данными хешируемого компонента в формате JSON. </param>
        /// <returns> Десериализованный хешируемый компонент. </returns>
        public static IHashable Deserialize(Type type, string json)
        {
            // TODO: Добавить проверку type, что он реализует IHashable.

            var jsonFormatter = new DataContractJsonSerializer(type);

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var deserializer = new DataContractJsonSerializer(type);
                var result = (IHashable)deserializer.ReadObject(ms);
                return result;
            }
        }
    }
}
