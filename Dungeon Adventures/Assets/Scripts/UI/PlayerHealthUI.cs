using System;
using Core;
using TMPro;
using Uitility;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    private GameObject _healthBar;
    private TextMeshProUGUI _healthTextCmp;

    private void Awake()
    {
        _healthBar = GameObject.FindGameObjectWithTag(Constants.PLAYER_HEALTH_BAR);

        _healthTextCmp = _healthBar.GetComponentInChildren<TextMeshProUGUI>();

        _healthTextCmp.text = String.Empty;
    }

    private void OnEnable()
    {
        EventManager.OnChangePlayerHealth += ChanglePlayerHealthHandler;
    }

    private void OnDisable()
    {
        EventManager.OnChangePlayerHealth -= ChanglePlayerHealthHandler;
    }

    private void ChanglePlayerHealthHandler(float playerHealthAmount)
    {
        _healthTextCmp.text = playerHealthAmount.ToString();
    }
}