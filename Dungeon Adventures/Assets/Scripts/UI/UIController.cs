using Core;
using DefaultNamespace;
using ScriptableObjects;
using Uitility;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]

    public class UIController : MonoBehaviour
    {
        public VisualElement RootVisualElement;
        public VisualElement PlayerInfoContainer;

        private VisualElement _keyItem;
        private UIBaseState _currentState;
        private UIDocument _uiDocumentCmp;
        private Label _healthAmount;
        private Label _potionsCount;

        private void Awake()
        {
            _uiDocumentCmp = GetComponent<UIDocument>();

            RootVisualElement = _uiDocumentCmp.rootVisualElement;

            PlayerInfoContainer = RootVisualElement.Q<VisualElement>
                (Constants.UI_TOOLKIT_VISUAL_ELEMENT_PLAYER_INFO_CONTAINER);

            _healthAmount = PlayerInfoContainer.Q<Label>
                (Constants.UI_TOOLKIT_LABEL_HEALTH_AMOUNT);

            _potionsCount = PlayerInfoContainer.Q<Label>
            (Constants.UI_TOOLKIT_LABEL_POTIONS_COUNT);

            _keyItem = PlayerInfoContainer.Q<VisualElement>
                (Constants.UI_TOOLKIT_VISUAL_ELEMENT_KEY_IMAGE);
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerHealth += HandlerChangePlayerHealth;
            EventManager.OnChangePlayerPotionCount += HandlerChangePlayerPotionCount;
            EventManager.OnPlayerGetItem += HandlerPlayerGetItem;
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandlerChangePlayerHealth;
            EventManager.OnChangePlayerPotionCount -= HandlerChangePlayerPotionCount;
            EventManager.OnPlayerGetItem -= HandlerPlayerGetItem;
        }

        private void HandlerChangePlayerHealth(float newAmount)
        {
            _healthAmount.text = newAmount.ToString();
        }

        private void HandlerChangePlayerPotionCount(int newCount)
        {
            _potionsCount.text = newCount.ToString();
        }

        private void HandlerPlayerGetItem(ItemSO item)
        {
            _keyItem.style.backgroundImage = new StyleBackground(item.Image);

            _keyItem.style.display = DisplayStyle.Flex;
        }
    }
}
