using System;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BoxCollider))]

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterStatsSO _playerStats;

        [NonSerialized] public List<ItemSO> Items = new List<ItemSO>();

        private Health _healthCmp;
        private Combat _combatCmp;
        private NavMeshAgent _agentCmp;

        private void Awake()
        {
            _healthCmp = GetComponent<Health>();

            _combatCmp = GetComponent<Combat>();

            _agentCmp = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            EventManager.OnPlayerGetItem += HandlerPlayerGetItem;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerGetItem -= HandlerPlayerGetItem;
        }

        private void Start()
        {
            _healthCmp.HealthPoints = _playerStats.HealthPoints;

            _combatCmp.Damage = _playerStats.MeeleDamage;

            _agentCmp.speed = _playerStats.Speed;

            EventManager.RaiseChangePlayerHealth(_healthCmp.HealthPoints);

            EventManager.RaiseChangePlayerPotionCount(_healthCmp.PotionCount);
        }

        private void HandlerPlayerGetItem(ItemSO item)
        {
            Items.Add(item);
        }
    }
}
