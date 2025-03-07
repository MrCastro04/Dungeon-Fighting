using System;
using ScriptableObjects;
using UnityEngine;
using Utility;

namespace Core
{
    public static class EventManager
    {
        public static event Action<float> OnChangePlayerHealth;
        public static event Action <int> OnChangePlayerPotionCount;
        public static event Action<ItemSO> OnPlayerGetItem;
        public static event Action <Collider , int> OnPortalEnter;
        public static event Action OnAbilityButtonClick;
        public static event Action OnPlayerAbilityReady;
        public static event Action OnEnterLockDoor;
        public static event Action OnExitLockDoor;
        public static event Action OnBossEnterSecondPhase;
        public static event Action OnChangeBossHitCounters;
        public static event Action OnPlayerEnterTheAreaWithBoss;
        public static event Action OnStartButtonClick;
        public static event Action OnGameEnd;
        public static event Action <Actions> OnSoundHit;
        public static event Action <Actions> OnSoundUsePotion;
        public static event Action<Actions> OnSoundDefeat;

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

        public static void RaiseOnPlayerAbilityReady()
        {
            OnPlayerAbilityReady?.Invoke();
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

        public static void RaiseOnChangeBossHitCounters()
        {
            OnChangeBossHitCounters?.Invoke();
        }

        public static void RaiseOnPlayerEnterTheAreaWithBoss()
        {
            OnPlayerEnterTheAreaWithBoss?.Invoke();
        }

        public static void RaiseOnStartButtonClick()
        {
            OnStartButtonClick?.Invoke();
        }

        public static void RaiseOnGameEnd()
        {
            OnGameEnd?.Invoke();
        }

        public static void RaiseSoundOnHit(Actions actionType)
        {
            OnSoundHit?.Invoke(actionType);
        }

        public static void RaiseSoundOnUsePotion(Actions actionType)
        {
            OnSoundUsePotion?.Invoke(actionType);
        }

        public static void RaiseSoundOnDefeat(Actions actionType)
        {
            OnSoundDefeat?.Invoke(actionType);
        }
    }
}