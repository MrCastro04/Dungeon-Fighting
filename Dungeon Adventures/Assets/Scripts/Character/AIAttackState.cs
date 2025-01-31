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
            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                enemy.SwitchState(enemy.ChaseState);
                return;
            }

            enemy.transform.LookAt(enemy.Player.transform.position);
        }
    }
}