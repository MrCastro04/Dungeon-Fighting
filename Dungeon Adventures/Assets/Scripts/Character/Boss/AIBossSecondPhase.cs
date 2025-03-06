using Character.BaseEnemy;

namespace Character.Boss
{
    public class AIBossSecondPhase : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            (enemy as BossController)?.BossCombatCmp.CancelAttack();
            (enemy as BossController)?.BossAbilityCmp.SetAbilityToken(true);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.Player == null)
            {
                (enemy as BossController)?.BossCombatCmp.CancelAttack();

                return;
            }

            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                (enemy as BossController)?.BossCombatCmp.CancelAttack();

                enemy.SwitchState(enemy.ChaseState);

                return;
            }

            enemy.transform.LookAt(enemy.Player.transform.position);

            (enemy as BossController)?.BossCombatCmp.StartAttack();
        }
    }
}
