using System;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public static class EventManager
    {
        public static event Action<float> OnChangePlayerHealth;
        public static event Action <int> OnChangePlayerPotionCount;
        public static event Action<ItemSO> OnPlayerGetItem;
        public static event Action<Collider , int> OnPortalEnter;

        public static void RaiseChangePlayerHealth(float newHealthAmount)
        {
            OnChangePlayerHealth?.Invoke(newHealthAmount);
        }

        public static void RaiseChangePlayerPotionCount(int newAmount)
        {
            OnChangePlayerPotionCount?.Invoke(newAmount);
        }

        public static void RaisePlayerGetItem(ItemSO item)
        {
            OnPlayerGetItem?.Invoke(item);
        }

        public static void RaiseOnPortalEnter(Collider player, int sceneIndex)
        {
            OnPortalEnter?.Invoke(player, sceneIndex);
        }
    }
}