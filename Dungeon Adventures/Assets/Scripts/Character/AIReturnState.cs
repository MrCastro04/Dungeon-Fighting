namespace Character
{
    public class AIReturnState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            if (enemy.transform.position != enemy.OriginalPosition)
            {
                enemy.Movement.MoveAgentByDestination(enemy.OriginalPosition);
            }

            else
            {
                if (enemy.transform.rotation != enemy.OriginalRotation)
                {

                }
            }
        }

        public override void UpdateState(EnemyController enemy)
        {

        }
    }
}