using System;
using Core;
using TMPro;
using Uitility;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHealth : MonoBehaviour
{
    [NonSerialized] public Slider SliderCmp;

    private Canvas _canvasCmp;
    private GameObject _healthBar;
    private TextMeshProUGUI _healthTextCmp;

    private void Awake()
    {
        _canvasCmp = GetComponent<Canvas>();

        _healthBar = _canvasCmp.GetComponentInChildren<GameObject>();

        _healthTextCmp = _healthBar.GetComponentInChildren<TextMeshProUGUI>();

        _healthTextCmp.text = String.Empty;
    }

    private void OnEnable()
    {
        if (CompareTag(Constants.PLAYER_TAG))
        {
            EventManager.OnChangePlayerHealth += ChanglePlayerHealthHandler;
        }
    }

    private void OnDisable()
    {
        if (CompareTag(Constants.PLAYER_TAG))
        {
            EventManager.OnChangePlayerHealth -= ChanglePlayerHealthHandler;
        }
    }

    private void ChanglePlayerHealthHandler(float playerHealthAmount)
    {
        _healthTextCmp.text = playerHealthAmount.ToString();
    }
}
