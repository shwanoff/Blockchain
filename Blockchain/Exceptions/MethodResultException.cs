using System;

namespace Blockchain.Exceptions
{
    /// <summary>
    /// Представляет ошибки постусловия метода. 
    /// Выбрасывается, если результирующий аргумент не соответствует необходимым условиям.
    /// </summary>
    public class MethodResultException : ArgumentException
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса MethodResultException 
        /// с указанным сообщением об ошибке и именем параметра, ставшего причиной исключения. 
        /// </summary>
        /// <param name="paramName"> Имя параметра. </param>
        public MethodResultException(string paramName, string message)
            : base($"Аргумент {paramName} не соответствует постусловиям. {message}", paramName)
        {
        }
    }
}
