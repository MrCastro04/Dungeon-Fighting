using UnityEngine;

namespace Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            Debug.Log("аавіаовіаоівлао");
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