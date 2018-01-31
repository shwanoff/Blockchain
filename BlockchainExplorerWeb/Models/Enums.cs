using System;
using System.ComponentModel;

namespace BlockchainExplorerWeb
{
    /// <summary>
    /// Используемые перечисления.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Права доступа пользователя.
        /// </summary>
        public enum UserRole : byte
        {
            /// <summary>
            /// Администратор.
            /// </summary>
            [Description("Полный доступ")]
            Admin = 1,

            /// <summary>
            /// Права на запись.
            /// </summary>
            [Description("Запись")]
            Writer = 2,

            /// <summary>
            /// Права на чтение.
            /// </summary>
            [Description("Чтения")]
            Reader = 3
        }

        /// <summary>
        /// Тип хранимых в блоке данных.
        /// </summary>
        public enum DataType : byte
        {
            /// <summary>
            /// Информация.
            /// </summary>
            [Description("Хранимые данные")]
            Content = 1,

            /// <summary>
            /// Сведения о пользователях.
            /// </summary>
            [Description("Данные о пользователе")]
            User = 2,

            /// <summary>
            /// Сведения о сервере.
            /// </summary>
            [Description("Данные о хосте")]
            Node = 3
        }

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

            var memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }
    }
}