using Character.BaseEnemy;

namespace Character.Boss
{
    public class AIBossSecondPhase : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            if (enemy is BossController bossController)
            {
                ApplyBossHit(bossController);
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy is BossController bossController)
            {
                if (enemy.Player == null)
                {
                    bossController.BossCombatCmp.CancelAttack();

                    return;
                }

                if (enemy.DistanceFromPlayer > enemy.AttackRange)
                {
                    bossController.BossCombatCmp.CancelAttack();

                    enemy.SwitchState(enemy.ChaseState);

                    return;
                }

                enemy.transform.LookAt(enemy.Player.transform.position);

                ApplyBossHit(bossController);
            }
        }

        private void ApplyBossHit(BossController bossController)
        {
            if (bossController.BossAbilityCmp.IsCurrentCountersEqualOrGreatThenDesired())
            {
                bossController.BossAbilityCmp.SetAbilityToken(true);
            }

            else
            {
                bossController.BossCombatCmp.StartAttack();
            }
        }
    }
}