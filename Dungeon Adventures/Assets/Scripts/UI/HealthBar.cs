using System;
using Core;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
   private TextMeshProUGUI _healthText;

   private void Awake()
   {
      _healthText = GetComponentInChildren<TextMeshProUGUI>();

      _healthText.text = String.Empty;
   }

   private void OnEnable()
   {
      EventManager.OnChangePlayerHealth += ChangePlayerHealthHandler;
   }

   private void OnDisable()
   {
      EventManager.OnChangePlayerHealth -= ChangePlayerHealthHandler;
   }

   private void ChangePlayerHealthHandler(float healthAmount)
   {
      _healthText.text = healthAmount.ToString();
   }
}
