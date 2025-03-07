using Core;
using DefaultNamespace;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class UIEndGameState : UIBaseState
    {
        private VisualElement _endPanelContainer;
        private Button _restartButton;
        private Button _menuButton;

        public UIEndGameState(UIController controller) : base(controller) { }

        public override void EnterState()
        {
            PlayerInput playerInput = GameObject.FindGameObjectWithTag(Constants.TAG_GAME_MANAGER)
                .GetComponent<PlayerInput>();

            playerInput.SwitchCurrentActionMap(Constants.ACTION_MAP_UI);

           _endPanelContainer = Controller.RootVisualElement.Q<VisualElement>
               (Constants.UI_TOOLKIT_VISUAL_ELEMENT_END_PANEL_CONTAINER);

           _menuButton = _endPanelContainer.Q<Button>(Constants.UI_TOOLKIT_MENU_BUTTON);

           _restartButton = _endPanelContainer.Q<Button>(Constants.UI_TOOLKIT_RESTART_BUTTON);

           _endPanelContainer.style.display = DisplayStyle.Flex;

           Controller.Buttons.Clear();

           Controller.Buttons.Add(_restartButton);

           Controller.Buttons.Add(_menuButton);

           Controller.Buttons[0].AddToClassList(Constants.UI_TOOLKIT_CLASS_STYLE_ACTIVE_BUTTON);
        }

        public override void SelectButton()
        {
            Button currentButton = Controller.Buttons[Controller.CurrentSelection];

            if (currentButton.name == "restart-button")
            {
                SceneTransition.Initiate(1);
            }

            else if (currentButton.name == "menu-button")
            {
                SceneTransition.Initiate(0);
            }
        }
    }
}
