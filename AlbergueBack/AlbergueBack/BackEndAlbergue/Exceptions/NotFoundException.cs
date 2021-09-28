using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BackEndAlbergue.Exceptions
{
    
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
