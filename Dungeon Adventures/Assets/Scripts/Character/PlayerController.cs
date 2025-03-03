using System;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using Uitility;
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

        public Health HealthCmp => _healthCmp;
        public Combat CombatCmp => _combatCmp;
        public NavMeshAgent AgentCmp => _agentCmp;

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
            if (PlayerPrefs.HasKey(Constants.PREF_PLAYER_HEALTH))
            {
                _healthCmp.HealthPoints = PlayerPrefs.GetFloat(Constants.PREF_PLAYER_HEALTH);

                _combatCmp.Damage = PlayerPrefs.GetFloat(Constants.PREF_PLAYER_DAMAGE);

                _agentCmp.speed = PlayerPrefs.GetFloat(Constants.PREF_PLAYER_SPEED);

                _healthCmp.PotionCount = PlayerPrefs.GetInt(Constants.PREF_PLAYER_POTION_COUNT);
            }

            else
            {
                _healthCmp.HealthPoints = _playerStats.HealthPoints;

                _combatCmp.Damage = _playerStats.MeeleDamage;

                _agentCmp.speed = _playerStats.Speed;
            }

            EventManager.RaiseChangePlayerHealth(_healthCmp.HealthPoints);

            EventManager.RaiseChangePlayerPotionCount(_healthCmp.PotionCount);

            if (Items.Count == 0)
            {
                Debug.Log("List is empty");
            }
        }

        private void HandlerPlayerGetItem(ItemSO item)
        {
            Items.Add(item);
        }
    }
}
