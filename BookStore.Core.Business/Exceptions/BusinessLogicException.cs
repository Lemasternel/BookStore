using System;

namespace BookStore.Core.Business.Exceptions
{
    public class BusinessLogicException : Exception
    {

        private string _message;
        public override string Message => _message;

        public BusinessLogicException(string message)
        {
            _message = (string.Format(Resources.Messages.BusinessLogicError, message));
        }
    }
}
