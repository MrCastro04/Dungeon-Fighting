using System;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Core
{
    public static class EventManager
    {
        public static event Action<float> OnChangePlayerHealth;
        public static event Action<int> OnChangePlayerPotionCount;
        public static event Action<ItemSO> OnPlayerGetItem;
        public static event Action<Collider, int> OnPortalEnter;
        public static event Action<SoundActionType, IControllerType> OnSoundHit;
        public static event Action<SoundActionType, IControllerType> OnSoundUsePotion;
        public static event Action<SoundActionType, IControllerType> OnSoundDefeat;
        public static event Action<SoundActionType, IControllerType> OnSoundMissHit;
        public static event Action<SoundActionType, IControllerType> OnSoundAbilityWindCut;
        public static event Action OnAbilityButtonClick;
        public static event Action OnPlayerAbilityReady;
        public static event Action OnEnterLockDoor;
        public static event Action OnExitLockDoor;
        public static event Action OnBossEnterSecondPhase;
        public static event Action OnChangeBossHitCounters;
        public static event Action OnPlayerEnterTheAreaWithBoss;
        public static event Action OnStartButtonClick;
        public static event Action OnGameEnd;


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

        public static void RaiseSoundOnHit(SoundActionType actionType, IControllerType controllerType)
        {
            OnSoundHit?.Invoke(actionType, controllerType);
        }

        public static void RaiseSoundOnMissHit(SoundActionType actionType, IControllerType controllerType)
        {
            OnSoundMissHit?.Invoke(actionType, controllerType);
        }

        public static void RaiseSoundOnUsePotion(SoundActionType actionType, IControllerType controllerType)
        {
            OnSoundUsePotion?.Invoke(actionType, controllerType);
        }

        public static void RaiseSoundOnDefeat(SoundActionType actionType, IControllerType controllerType)
        {
            OnSoundDefeat?.Invoke(actionType, controllerType);
        }

        public static void RaiseSoundOnAbilityWindCut(SoundActionType actionType , IControllerType controllerType)
        {
            OnSoundAbilityWindCut?.Invoke(actionType , controllerType);
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
    }
}