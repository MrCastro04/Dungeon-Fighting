using Character.BaseEnemy;
using ScriptableObjects;
using UnityEngine;

namespace Character.Range_Enemy
{
    [RequireComponent(typeof(RangeCombat))]

    public class EnemyMageController : EnemyController
    {
        private RangeCombat _rangeCombatCmp;
        
        public RangeCombat RangeCombatCmp => _rangeCombatCmp;

        protected override void Awake()
        {
            base.Awake();

            _rangeCombatCmp = GetComponent<RangeCombat>();
        }

        protected override void Start()
        {
            if (_enemyStats is RangeCharacterStatsSO rangeCharacterStats)
            {
                base.Start();

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
