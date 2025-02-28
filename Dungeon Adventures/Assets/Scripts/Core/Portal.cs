using System;
using System.Collections.Generic;
using Character;
using ScriptableObjects;
using Uitility;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] private ItemSO _itemToEnterThePortal;
         [SerializeField] private Transform _spawnPoint;
         [SerializeField] private int _nextScene;

         private Collider _colliderCmp;

         private void Awake()
         {
             _colliderCmp = GetComponent<Collider>();
         }

         private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag(Constants.TAG_PLAYER)) return;

            if (CheckPlayerHasKey(other))
            {
                _colliderCmp.enabled = false;

                EventManager.RaisePortalEnter(other, _nextScene);

                SceneTransition.Initiate(_nextScene);
            }

            else
            {
                EventManager.RaiseOnEnterLockDoor();
            }
        }

         private void OnTriggerExit(Collider other)
         {
             if(!other.CompareTag(Constants.TAG_PLAYER)) return;

             if (CheckPlayerHasKey(other))
             {
                 return;
             }

             EventManager.RaiseOnExitLockDoor();
         }

         private bool CheckPlayerHasKey(Collider player)
         {
             bool HasKey = false;

             PlayerController playerController = player.GetComponent<PlayerController>();

             List<ItemSO> playerItems = playerController.Items;

             playerItems.ForEach((ItemSO item) =>
             {
                 if (item.Name == _itemToEnterThePortal.Name)
                 {
                     HasKey = true;
                 }
             });

             return HasKey;
         }
    }
}
