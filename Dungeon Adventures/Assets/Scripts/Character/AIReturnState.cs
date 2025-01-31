namespace Character
{
    public class AIReturnState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementCmp.MoveAgentByDestination(enemy.OriginalPosition);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.DistanceFromPlayer <= enemy.ChaseRange)
            {
                enemy.SwitchState(enemy.ChaseState);
                return;
            }

            
        }
    }
}