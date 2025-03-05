using Character.Range_Enemy;

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

                else
                {
                    enemy.CombatCmp.CancelAttack();
                }

                return;
            }


            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                if (enemy.CombatCmp != null)
                {
                    enemy.CombatCmp.CancelAttack();
                }

                else
                {
                    (enemy as EnemyMageController)?.RangeCombatCmp.CancelAttack();
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
                (enemy as EnemyMageController)?.RangeCombatCmp.StartAttack();
            }
        }
    }
}