using System;
using UnityEngine;

public class RangeCombat : MonoBehaviour
{
 [NonSerialized] public float RangeDamage;
 [NonSerialized] public float FireRate;
 [NonSerialized] public float NextFireTime;
 [NonSerialized] public float ProjectileSpeed;

 public void StartAttack()
 {
  Debug.Log("Range Combat Start Attack");
 }

 public void CancelAttack()
 {
  Debug.Log("Range Combat Cancel Attack");
 }
}
