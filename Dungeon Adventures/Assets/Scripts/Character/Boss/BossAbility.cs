using Character.Player;
using Core;

namespace Character.Boss
{
    public class BossAbility : Ability
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            EventManager.OnBossEnterSecondPhase += HandlerEnterSecondPhase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventManager.OnBossEnterSecondPhase -= HandlerEnterSecondPhase;
        }

        private void HandlerEnterSecondPhase()
        {
            this.SetAbilityToken(true);
        }
    }
}