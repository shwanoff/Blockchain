using System;

namespace Blockchain.Exceptions
{
    /// <summary>
    /// Представляет ошибки предусловий метода. 
    /// Выбрасывается, если передаваемый аргумент не соответствует необходимым условиям.
    /// </summary>
    public class MethodRequiresException : ArgumentException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса MethodRequiresException 
        /// с указанным сообщением об ошибке и именем параметра, ставшего причиной исключения. 
        /// </summary>
        /// <param name="paramName"> Имя параметра. </param>
        public MethodRequiresException(string paramName, string message) 
            : base($"Аргумент {paramName} не соответствует предусловиям. {message}", paramName)
        {
        }
    }
}
