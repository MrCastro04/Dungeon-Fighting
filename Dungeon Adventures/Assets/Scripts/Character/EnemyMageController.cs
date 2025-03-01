using ScriptableObjects;
using UnityEngine;

namespace Character.Mage
{
    [RequireComponent(typeof(RangeCombat))]
    public class EnemyMageController : EnemyController
    {
        private RangeCombat _rangeCombatCmp;

        public override void Awake()
        {
            base.Awake();

            _rangeCombatCmp = GetComponent<RangeCombat>();
        }

        public override void Start()
        {
            _currentState.EnterState(this);

            if (_enemyStats is RangeCharacterStatsSO rangeCharacterStats)
            {
                HealthCmp.HealthPoints = _enemyStats.healthPoints;

                MovementCmp.NavMeshAgent.speed = _enemyStats.speed;

                HealthCmp.SliderCmp.maxValue = HealthCmp.HealthPoints;

                HealthCmp.SliderCmp.value = HealthCmp.HealthPoints;

                _rangeCombatCmp.RangeDamage = rangeCharacterStats.ProjectileDamage;

                _rangeCombatCmp.FireRate = rangeCharacterStats.FireRate;

                _rangeCombatCmp.NextFireTime = rangeCharacterStats.NextFireTime;

                _rangeCombatCmp.ProjectileSpeed = rangeCharacterStats.ProjectileSpeed;
            }

            else
            {
                Debug.LogWarning($"You choose no RangeCharacterStatsSO for this {this.name}." +
                                        "Stats does not registered." +
                                        " Set correct Stats - RangeCharacterStatsSO.");
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
