using System;

namespace Dwapi.Vmmc.SharedKernel.Exceptions
{
    public class SubscriberNotFoundException : Exception
    {
        public SubscriberNotFoundException(string name) : base($"Subscriber {name} not found")
        {

        }
    }
}