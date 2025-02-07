namespace Character
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
                enemy.CombatCmp.CancelAttack();

                return;
            }

            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                enemy.CombatCmp.CancelAttack();

                enemy.SwitchState(enemy.ChaseState);

                return;
            }

            enemy.transform.LookAt(enemy.Player.transform.position);

            enemy.CombatCmp.StartAttack();
        }
    }
}