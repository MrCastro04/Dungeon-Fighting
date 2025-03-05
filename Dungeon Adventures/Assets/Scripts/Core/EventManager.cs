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
        public static event Action <Collider , int> OnPortalEnter;
        public static event Action OnAbilityButtonClick;
        public static event Action OnAbilityReady;
        public static event Action OnEnterLockDoor;
        public static event Action OnExitLockDoor;
        public static event Action OnBossEnterSecondPhase;


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

        public static void RaisePortalEnter(Collider player, int sceneIndex)
        {
            OnPortalEnter?.Invoke(player, sceneIndex);
        }

        public static void RaisebilityButtonClick()
        {
            OnAbilityButtonClick?.Invoke();
        }

        public static void RaiseOnAbilityReady()
        {
            OnAbilityReady?.Invoke();
        }

        public static void RaiseOnEnterLockDoor()
        {
            OnEnterLockDoor?.Invoke();
        }

        public static void RaiseOnExitLockDoor()
        {
            OnExitLockDoor?.Invoke();
        }

        public static void RaiseOnBossEnterSecondPhase()
        {
            OnBossEnterSecondPhase?.Invoke();
        }
    }
}