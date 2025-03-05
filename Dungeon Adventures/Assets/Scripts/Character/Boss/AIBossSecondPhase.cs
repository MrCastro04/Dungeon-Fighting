using Character.BaseEnemy;
using Character.Boss;

public class AIBossSecondPhase : AIBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        (enemy as BossController)?.BossCombatCmp.CancelAttack();
       (enemy as BossController)?.BossAbilityCmp.SetAbilityToken(true);
    }

    public override void UpdateState(EnemyController enemy)
    {

    }
}
