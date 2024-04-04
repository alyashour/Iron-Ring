using System;
using UnityEngine.Events;

namespace Global
{
    [Serializable]
    public class CountableEvent : UnityEvent
    {
        public int InvokeCount
        {
            get;

            private set;
        }

        public new void Invoke()
        {
            InvokeCount++;
            base.Invoke();
        }
    }
}