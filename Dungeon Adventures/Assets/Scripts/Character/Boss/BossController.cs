using Character.BaseEnemy;
using UnityEngine;

namespace Character.Boss
{
    [RequireComponent(typeof(BossCombat))]
    [RequireComponent(typeof(BossAbility))]

    public class BossController : EnemyController
    {
        public AIBossSecondPhase AiBossSecondPhase = new();

        private BossCombat _bossCombatCmp;
        private BossAbility _bossAbilityCmp;

        public BossCombat BossCombatCmp => _bossCombatCmp;
        public BossAbility BossAbilityCmp => _bossAbilityCmp;

        protected override void Awake()
        {
            base.Awake();

            _bossCombatCmp = GetComponent<BossCombat>();

            _bossAbilityCmp = GetComponent<BossAbility>();
        }
    }
}
