using Character.BaseEnemy;

namespace Character.Boss
{
    public class AIBossSecondPhase : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            if (enemy is BossController bossController)
            {
                if (bossController.BossAbilityCmp.IsCurrentCountersEqualOrGreatThenDesired())
                {
                    bossController.BossAbilityCmp.SetAbilityToken(true);
                }
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

                if (bossController.BossAbilityCmp.IsCurrentCountersEqualOrGreatThenDesired())
                {
                    bossController.BossAbilityCmp.SetAbilityToken(true);
                }

                enemy.transform.LookAt(enemy.Player.transform.position);

                bossController.BossCombatCmp.StartAttack();
            }
        }
    }
}