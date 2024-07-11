using System.Runtime.Serialization;

namespace Booking.Domain.Exception
{
    [Serializable]
    public class NotFoundException : CoreBusinessException
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
