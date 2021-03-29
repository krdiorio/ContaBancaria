using System;
using System.Collections.Generic;
using System.Text;

namespace ContaBancaria.Entities.Exceptions
{
    class DomainException : ApplicationException
    {
        public DomainException(string mensagem) : base(mensagem)
        {
        }


    }
}
