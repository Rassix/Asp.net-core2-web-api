using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace DatingApp.API.Exceptions
{
    [Serializable]
    public class UserUpdateException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public UserUpdateException() {}
        public UserUpdateException(string message) : base(message) {}
        public UserUpdateException(string message, Exception exception) : base(message, exception) {}

        protected UserUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
