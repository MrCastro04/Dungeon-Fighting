using ScriptableObjects;
using Uitility;
using UnityEngine;

namespace Character.Mage
{
    [RequireComponent(typeof(RangeCombat))]
    public class EnemyMageController : EnemyController
    {
        private RangeCombat _rangeCombatCmp;

        public override void Awake()
        {
            _currentState = _chaseState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.TAG_PLAYER);

            MovementCmp = GetComponent<Movement>();

            HealthCmp = GetComponent<Health>();

            _rangeCombatCmp = GetComponent<RangeCombat>();
        }

        public override void Start()
        {
            _currentState.EnterState(this);

            if (_enemyStats is RangeCharacterStatsSO rangeCharacterStatsSo)
            {
                HealthCmp.HealthPoints = _enemyStats.healthPoints;

                MovementCmp.NavMeshAgent.speed = _enemyStats.speed;

                HealthCmp.SliderCmp.maxValue = HealthCmp.HealthPoints;

                HealthCmp.SliderCmp.value = HealthCmp.HealthPoints;

                _rangeCombatCmp.RangeDamage = rangeCharacterStatsSo.ProjectileDamage;

                _rangeCombatCmp.FireRate = rangeCharacterStatsSo.FireRate;

                _rangeCombatCmp.NextFireTime = rangeCharacterStatsSo.NextFireTime;

                _rangeCombatCmp.ProjectileSpeed = rangeCharacterStatsSo.ProjectileSpeed;
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
