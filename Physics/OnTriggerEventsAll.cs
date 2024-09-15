using UnityEngine;

namespace _UTIL_
{
    public class OnTriggerEventsAll : OnTriggerEvents
    {
        private void OnTriggerStay(Collider other)
        {
            onTriggerEvent?.Invoke(other, Types.Stay);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            onTriggerEvent2D?.Invoke(other, Types.Stay);
        }
    }
}