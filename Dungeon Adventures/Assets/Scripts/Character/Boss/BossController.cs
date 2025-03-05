using Character.BaseEnemy;
using UnityEngine;

namespace Character.Boss
{
    [RequireComponent(typeof(BossCombat))]

    public class BossController : EnemyController
    {
        private BossCombat _bossCombatCmp;

        public BossCombat BossCombatCmp => _bossCombatCmp;

        protected override void Awake()
        {
            base.Awake();

            _bossCombatCmp = GetComponent<BossCombat>();
        }
    }
}
