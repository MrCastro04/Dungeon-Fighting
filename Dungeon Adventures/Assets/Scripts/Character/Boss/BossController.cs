using Character.BaseEnemy;
using Core;
using UnityEngine;

namespace Character.Boss
{
    [RequireComponent(typeof(BossCombat))]
    [RequireComponent(typeof(BossAbility))]

    public class BossController : EnemyController
    {
        private AIBossSecondPhase _aiBossSecondPhase = new();

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

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.OnBossEnterSecondPhase += HandlerBossEnterSecondPhase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.OnBossEnterSecondPhase -= HandlerBossEnterSecondPhase;
        }

        private void HandlerBossEnterSecondPhase()
        {
            SwitchState(_aiBossSecondPhase);
        }
    }
}
