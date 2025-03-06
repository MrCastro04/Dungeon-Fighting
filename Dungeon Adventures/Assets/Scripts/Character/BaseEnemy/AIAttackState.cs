using Character.Boss;
using Character.Range_Enemy;
using Core;

namespace Character.BaseEnemy
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementCmp.StopAgentMoving();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.Player == null)
            {
                if (enemy is EnemyMageController mageController)
                {
                    mageController.RangeCombatCmp.CancelAttack();
                }

                else if (enemy is BossController bossController)
                {
                    bossController.BossCombatCmp.CancelAttack();
                }

                else
                {
                    enemy.CombatCmp.CancelAttack();
                }

                return;
            }

            if (enemy is BossController boss)
            {
                if (boss.HealthCmp.IsHealthLesserRequiredPercentage(boss, 0.5f))
                {
                    boss.BossCombatCmp.CancelAttack();

                    EventManager.RaiseOnBossEnterSecondPhase();

                    return;
                }
            }

            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                if (enemy.CombatCmp != null)
                {
                    enemy.CombatCmp.CancelAttack();
                }

                else
                {
                    if (enemy is EnemyMageController enemyMageController)
                    {
                        enemyMageController.RangeCombatCmp.CancelAttack();
                    }

                    else if (enemy is BossController bossController)
                    {
                        bossController.BossCombatCmp.CancelAttack();
                    }
                }

                enemy.SwitchState(enemy.ChaseState);

                return;
            }

            enemy.transform.LookAt(enemy.Player.transform.position);

            if (enemy.CombatCmp != null)
            {
                enemy.CombatCmp.StartAttack();
            }

            else
            {
                if (enemy is EnemyMageController enemyMageController)
                {
                    enemyMageController.RangeCombatCmp.StartAttack();
                }

                else if (enemy is BossController bossController)
                {
                    bossController.BossCombatCmp.StartAttack();
                }
            }
        }
    }
}