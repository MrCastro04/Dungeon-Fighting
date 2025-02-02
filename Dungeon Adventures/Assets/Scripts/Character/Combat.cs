using Uitility;
using Character;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
   [NonSerialized] public float damage;
   [NonSerialized] public bool isAttacking = false;

   private BubbleEvent _bubbleEvent;
   private Animator _animator;

   private void OnEnable()
   {
      _bubbleEvent.OnBubbleStartAttack += HandleBubbleStartAttack;

      _bubbleEvent.OnBubbleEndAttack += HandleBubbleEndAttack;

      _bubbleEvent.OnBubblHitAttack += HandleBubbleHitAttack;
   }

   private void OnDisable()
   {
      _bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;

      _bubbleEvent.OnBubbleEndAttack -= HandleBubbleEndAttack;

      _bubbleEvent.OnBubblHitAttack -= HandleBubbleHitAttack;
   }

   private void Awake()
   {
      _animator = GetComponentInChildren<Animator>();

      _bubbleEvent = GetComponentInChildren<BubbleEvent>();
   }

   public void HandleAttack(InputAction.CallbackContext context)
   {
      if(context.performed == false) return;

      StartAttack();
   }

   private void StartAttack()
   {
     if(isAttacking) return;

    _animator.SetFloat(Constants.SPEED_ANIMATOR_PARAM, 0f);

    _animator.SetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
   }

   private void CancelAttack()
   {
      _animator.ResetTrigger(Constants.ATTACK_ANIMATOR_PARAM);
   }

   private void HandleBubbleStartAttack()
   {
      isAttacking = true;
   }

   private void HandleBubbleEndAttack()
   {
      isAttacking = false;
   }

   private void HandleBubbleHitAttack()
   {
      RaycastHit[] targets = Physics.BoxCastAll(

         transform.position + transform.forward,

         transform.forward / 2,

         transform.forward,

         transform.rotation,

         1f);

      foreach (var target in targets)
      {
         if (CompareTag(gameObject.tag))
         {
            continue;
         }

         Health health = target.transform.gameObject.GetComponent<Health>();

         if (health == null)
         {
            continue;
         }

         health.TakeDamage(damage);
      }
   }
}

