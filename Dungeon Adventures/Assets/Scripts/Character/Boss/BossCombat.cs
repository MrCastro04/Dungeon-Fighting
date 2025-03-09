using Character.FOR_ALL_CHARACTERS;
using Core;
using Interfaces;
using UnityEngine;
using Utility;

namespace Character.Boss
{
    public class BossCombat : Combat
    {
        protected override void OnEnable()
        {
            _bubbleEvent.OnBubbleHitAttack += HandleBubbleHitAttack;
        }

        protected override void OnDisable()
        {
            _bubbleEvent.OnBubbleHitAttack -= HandleBubbleHitAttack;
        }

        protected override void HandleBubbleHitAttack()
        {
            EventManager.RaiseSoundOnMissHit( SoundActionType.MissHit,
                GetComponent<IControllerType>().GetSelfType() );

            var targets = Physics.BoxCastAll(

                transform.position + transform.forward,

                transform.forward / 2f,

                transform.forward,

                transform.rotation,

                3f);

            foreach (var target in targets)
            {
                if (CompareTag(target.transform.tag)) continue;

                var health = target.transform.gameObject.GetComponent<Health>();

                if (health == null) continue;

                health.TakeDamage(Damage);
            }
        }
    }
}
