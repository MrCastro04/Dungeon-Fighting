using System;
using System.Collections.Generic;
using Core;
using DefaultNamespace;
using ScriptableObjects;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]

    public class UIController : MonoBehaviour
    {
        [NonSerialized] public int CurrentSelection = 0;

        public UIEndGameState EndGameState;
        public UIMainMenuState MainMenuState;
        public VisualElement RootVisualElement;
        public VisualElement PlayerInfoContainer;
        public VisualElement MainMenuContainer;
        public VisualElement PlayerAbilityContainer;
        public List<Button> Buttons = new ();

        private UIBaseState _currentState;
        private VisualElement _keyItem;
        private VisualElement _playerAbilityIcon;
        private UIDocument _uiDocumentCmp;
        private Label _healthAmount;
        private Label _potionsCount;
        private Label _keyNotFoundMessage;

        private void Awake()
        {
            _uiDocumentCmp = GetComponent<UIDocument>();

            RootVisualElement = _uiDocumentCmp.rootVisualElement;

            MainMenuContainer = RootVisualElement.Q <VisualElement>
                (Constants.UI_TOOLKIT_VISUAL_ELEMENT_MAIN_MENU_CONTAINER );

            PlayerInfoContainer = RootVisualElement.Q<VisualElement>
                (Constants.UI_TOOLKIT_VISUAL_ELEMENT_PLAYER_INFO_CONTAINER);

            PlayerAbilityContainer = RootVisualElement.Q<VisualElement>
            (Constants.UI_TOOLKIT_VISUAL_ELEMENT_PLAYER_ABILITY_CONTAINER);

            _keyNotFoundMessage = RootVisualElement.Q<Label>(
                Constants.UI_TOOLKIT_LABEL_KEY_NOT_FOUND_MESSAGE);

            _healthAmount = PlayerInfoContainer.Q<Label>
                (Constants.UI_TOOLKIT_LABEL_HEALTH_AMOUNT);

            _potionsCount = PlayerInfoContainer.Q<Label>
            (Constants.UI_TOOLKIT_LABEL_POTIONS_COUNT);

            _keyItem = PlayerInfoContainer.Q<VisualElement>
                (Constants.UI_TOOLKIT_VISUAL_ELEMENT_KEY_IMAGE);

            _playerAbilityIcon = PlayerAbilityContainer.Q<VisualElement>
                (Constants.UI_TOOLKIT_VISUAL_ELEMENT_PLAYER_ABILITY_ICON);

            MainMenuState = new(this);

            EndGameState = new(this);
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerHealth += HandlerChangePlayerHealth;
            EventManager.OnChangePlayerPotionCount += HandlerChangePlayerPotionCount;
            EventManager.OnPlayerGetItem += HandlerPlayerGetItem;
            EventManager.OnAbilityButtonClick += HandlerAbilityClick;
            EventManager.OnPlayerAbilityReady += HandlerPlayerAbilityReady;
            EventManager.OnEnterLockDoor += HandlerEnterLockDoor;
            EventManager.OnExitLockDoor += HandlerExitLockDoor;
            EventManager.OnGameEnd += HandlerOnGameEnd;
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandlerChangePlayerHealth;
            EventManager.OnChangePlayerPotionCount -= HandlerChangePlayerPotionCount;
            EventManager.OnPlayerGetItem -= HandlerPlayerGetItem;
            EventManager.OnAbilityButtonClick -= HandlerAbilityClick;
            EventManager.OnPlayerAbilityReady -= HandlerPlayerAbilityReady;
            EventManager.OnEnterLockDoor -= HandlerEnterLockDoor;
            EventManager.OnExitLockDoor -= HandlerExitLockDoor;
            EventManager.OnGameEnd += HandlerOnGameEnd;
        }

        private void Start()
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            if (buildIndex == 0)
            {
                _currentState = MainMenuState;

                _currentState.EnterState();
            }

            else
            {
                PlayerInfoContainer.style.display = DisplayStyle.Flex;

                PlayerAbilityContainer.style.display = DisplayStyle.Flex;
            }
        }

        public void HandlerNavigate(InputAction.CallbackContext context)
        {
            if (context.performed == false || Buttons.Count == 0) return;

            Buttons[CurrentSelection].RemoveFromClassList(
                Constants.UI_TOOLKIT_CLASS_STYLE_ACTIVE_BUTTON);

            Vector2 input = context.ReadValue<Vector2>();

            CurrentSelection += input.y > 0 ? -1 : 1;

            CurrentSelection = Mathf.Clamp(CurrentSelection, 0, Buttons.Count - 1);

            Buttons[CurrentSelection].AddToClassList(
                Constants.UI_TOOLKIT_CLASS_STYLE_ACTIVE_BUTTON);
        }

        public void HandlerInteract(InputAction.CallbackContext context)
        {
            if(context.performed == false) return;

            _currentState.SelectButton();
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

        private void HandlerAbilityClick()
        {
            _playerAbilityIcon.AddToClassList(Constants.UI_TOOLKIT_CLASS_STYLE_LOW_TINT);
        }

        private void HandlerPlayerAbilityReady()
        {
            _playerAbilityIcon.RemoveFromClassList(Constants.UI_TOOLKIT_CLASS_STYLE_LOW_TINT);
        }

        private void HandlerEnterLockDoor()
        {
            _keyNotFoundMessage.style.display = DisplayStyle.Flex;
        }

        private void HandlerExitLockDoor()
        {
            _keyNotFoundMessage.style.display = DisplayStyle.None;
        }

        private void HandlerOnGameEnd()
        {
            _currentState = EndGameState;

            _currentState.EnterState();

        }
    }
}
