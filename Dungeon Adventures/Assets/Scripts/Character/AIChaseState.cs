using UnityEngine;

namespace Character
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
           enemy.MovementCmp.MoveAgentByDestination(enemy.Player.transform.position);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.DistanceFromPlayer < enemy.AttackRange)
            {
                enemy.SwitchState(enemy.AttackState);
                return;
            }

            Vector3 forwardVector = enemy.Player.transform.position - enemy.transform.position;

            forwardVector.y = 0f;

            enemy.MovementCmp.RotateAgentByOffset(forwardVector);
        }
    }
}