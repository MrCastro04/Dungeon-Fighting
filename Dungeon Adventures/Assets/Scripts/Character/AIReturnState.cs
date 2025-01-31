using UnityEngine;

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

            if (enemy.MovementCmp.ReachedDestination())
            {
                enemy.MovementCmp.RotateAgentByOffset(enemy.OriginalRotation);
                return;
            }

            Vector3 forwardVector = enemy.OriginalPosition - enemy.transform.position;

            enemy.MovementCmp.RotateAgentByOffset(forwardVector);
        }
    }
}