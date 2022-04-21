﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL.Exceptions
{
    public class NotManagerException : Exception
    {
        public NotManagerException()
        {
        }

        public NotManagerException(string? message) : base(message)
        {
        }

        public NotManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
