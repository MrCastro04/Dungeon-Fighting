using Character.FOR_ALL_CHARACTERS;
using Core;
using Interfaces;
using ScriptableObjects;
using Uitility;
using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Character.Player
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Ability))]
    [RequireComponent(typeof(CharacterSoundController))]

    public class PlayerController : MonoBehaviour , IHealthable , IControllerType
    {
        [SerializeField] private CharacterStatsSO _playerStats;

        private CharacterSoundController _characterSoundControllerCmp;
        private Health _healthCmp;
        private Combat _combatCmp;
        private NavMeshAgent _agentCmp;
        private Inventory _inventoryCmp;
        private Ability _abilityCmp;

        public Health HealthCmp => _healthCmp;
        public Combat CombatCmp => _combatCmp;
        public NavMeshAgent AgentCmp => _agentCmp;
        public Inventory InventoryCmp => _inventoryCmp;

        private void Awake()
        {
            _characterSoundControllerCmp = GetComponent<CharacterSoundController>();

            _healthCmp = GetComponent<Health>();

            _combatCmp = GetComponent<Combat>();

            _agentCmp = GetComponent<NavMeshAgent>();

            _inventoryCmp = GetComponent<Inventory>();

            _abilityCmp = GetComponent<Ability>();
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

                _healthCmp.OriginHealthPoints = HealthCmp.HealthPoints;

                _combatCmp.Damage = _playerStats.MeeleDamage;

                _agentCmp.speed = _playerStats.Speed;
            }

            EventManager.RaiseChangePlayerHealth(_healthCmp.HealthPoints);

            EventManager.RaiseChangePlayerPotionCount(_healthCmp.PotionCount);
        }

        public IControllerType GetSelfType()
        {
            return this;
        }
    }
}
