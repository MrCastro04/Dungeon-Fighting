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

        private Health _healthCmp;
        private Combat _combatCmp;
        private NavMeshAgent _agentCmp;

       private void Awake()
        {
            _healthCmp = GetComponent<Health>();

            _combatCmp = GetComponent<Combat>();

            _agentCmp = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _healthCmp.HealthPoints = _playerStats.healthPoints;

            _combatCmp.Damage = _playerStats.damage;

            _agentCmp.speed = _playerStats.speed;

            EventManager.RaiseChangePlayerHealth(_healthCmp.HealthPoints);

            EventManager.RaiseChangePlayerPotionCount(_healthCmp.PotionCount);
        }
    }
}
