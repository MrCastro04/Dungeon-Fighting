using System.Collections.Generic;
using Character;
using Character.Mage;
using Core;
using ScriptableObjects;
using Uitility;
using UnityEngine;

namespace Interacts
{
    public class ClosetDoor : Portal
    {
        [SerializeField] private ItemSO _itemToEnterThePortal;

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Fireball>())
            {
                return;
            }

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
            if (!other.CompareTag(Constants.TAG_PLAYER)) return;

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

            if (playerController == null)
                return false;

            List<ItemSO> playerItems = playerController.Items;

            playerItems.ForEach((ItemSO playerItem) =>
            {
                if (playerItem.Name == _itemToEnterThePortal.Name)
                {
                    HasKey = true;
                }
            });

            return HasKey;
        }
    }
}
