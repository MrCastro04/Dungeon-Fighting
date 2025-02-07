

using System;

namespace Core
{
    public static class EventManager
    {
        public static event Action<float> OnChangePlayerHealth;

        public static void RaiseChangePlayerHealth(float newHealthAmount)
        {
            OnChangePlayerHealth?.Invoke(newHealthAmount);
        }
    }
}