using UnityEngine;

namespace Character.BaseEnemy
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy) { }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.Player == null)
            {
                return;
            }

            if (enemy.DistanceFromPlayer < enemy.AttackRange)
            {
                enemy.SwitchState(enemy.AttackState);

                return;
            }

            enemy.MovementCmp.MoveAgentByDestination(enemy.Player.transform.position);

            Vector3 forwardVector = enemy.Player.transform.position - enemy.transform.position;

            forwardVector.y = 0f;

            enemy.MovementCmp.RotateAgentByOffset(forwardVector);
        }
    }
}