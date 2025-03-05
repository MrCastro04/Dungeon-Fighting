using Character.BaseEnemy;
using Core;

public class AIBossSecondPhase : AIBaseState
{
    public override void EnterState(EnemyController enemy)
    {
       EventManager.RaiseOnBossEnterSecondPhase();
    }

    public override void UpdateState(EnemyController enemy)
    {

    }
}
