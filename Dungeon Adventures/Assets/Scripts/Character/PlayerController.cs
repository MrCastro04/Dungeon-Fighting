using Core;
using ScriptableObjects;
using Uitility;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(BoxCollider))]

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterStatsSO _playerStats;

        private Health _healthCmp;
        private Combat _combatCmp;
        private NavMeshAgent _agentCmp;
        private Inventory _inventoryCmp;

        public Health HealthCmp => _healthCmp;
        public Combat CombatCmp => _combatCmp;
        public NavMeshAgent AgentCmp => _agentCmp;
        public Inventory InventoryCmp => _inventoryCmp;

        private void Awake()
        {
            _healthCmp = GetComponent<Health>();

            _combatCmp = GetComponent<Combat>();

            _agentCmp = GetComponent<NavMeshAgent>();

            _inventoryCmp = GetComponent<Inventory>();
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

            if (_inventoryCmp.Items.Count == 0)
            {
                Debug.Log("List is empty");
            }
        }
    }
}
