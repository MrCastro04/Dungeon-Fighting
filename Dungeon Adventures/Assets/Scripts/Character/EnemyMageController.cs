using ScriptableObjects;
using UnityEngine;

namespace Character.Mage
{
    [RequireComponent(typeof(RangeCombat))]

    public class EnemyMageController : EnemyController
    {
         private float _fireRate;
         private float _nextFireTime;

        private RangeCombat _rangeCombatCmp;

        public RangeCombat RangeCombatCmp => _rangeCombatCmp;

        protected override void Awake()
        {
            base.Awake();

            _rangeCombatCmp = GetComponent<RangeCombat>();
        }

        public override void Start()
        {
            _currentState.EnterState(this);

            if (_enemyStats is RangeCharacterStatsSO rangeCharacterStats)
            {
                HealthCmp.HealthPoints = _enemyStats.HealthPoints;

                MovementCmp.NavMeshAgent.speed = _enemyStats.Speed;

                AttackRange = _enemyStats.AttackRange;

                HealthCmp.SliderCmp.maxValue = HealthCmp.HealthPoints;

                HealthCmp.SliderCmp.value = HealthCmp.HealthPoints;

                _fireRate = rangeCharacterStats.FireRate;

                _nextFireTime = rangeCharacterStats.NextFireTime;

                _rangeCombatCmp.RangeDamage = rangeCharacterStats.ProjectileDamage;

                _rangeCombatCmp.ProjectileSpeed = rangeCharacterStats.ProjectileSpeed;
            }

            else
            {
                Debug.LogWarning($"You choose no RangeCharacterStatsSO for this {this.name}." +
                                 "Stats does not registered." +
                                 " Set correct Stats - RangeCharacterStatsSO.");
            }
        }
    }
}
