using System;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;

namespace Character
{
    public class Inventory : MonoBehaviour
    {
       [NonSerialized] public List<ItemSO> Items = new();

        private void OnEnable()
        {
            EventManager.OnPlayerGetItem += HandlerPlayerGetItem;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerGetItem -= HandlerPlayerGetItem;
        }

        public bool HasDesiredItem(ItemSO desiredItem)
        {
            bool hasItem = false;

            Items.ForEach((itemInList) =>
            {
                if (itemInList.Name == desiredItem.Name)
                {
                    hasItem = true;
                }
            });

            return hasItem;
        }

        private void HandlerPlayerGetItem(ItemSO item)
        {
            Items.Add(item);
        }
    }
}