using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealPotionViewer : MonoBehaviour
    {
        private TextMeshProUGUI _potionCountText;

        private void Awake()
        {
            _potionCountText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerPotionCount += HandlerChangePlayerPotionCount;
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerPotionCount -= HandlerChangePlayerPotionCount;
        }

        private void HandlerChangePlayerPotionCount(int newAmount)
        {
            _potionCountText.text = newAmount.ToString();
        }
    }
}
