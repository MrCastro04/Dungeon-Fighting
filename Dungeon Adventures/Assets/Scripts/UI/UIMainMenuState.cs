using Core;
using DefaultNamespace;
using UI;
using Uitility;
using UnityEngine.UIElements;

public class UIMainMenuState : UIBaseState
{
    public UIMainMenuState(UIController controller) : base(controller) {}

    public override void EnterState()
    {
        Controller.MainMenuContainer.style.display = DisplayStyle.Flex;

        Controller.Buttons = Controller.MainMenuContainer
            .Query<Button>(null, Constants.UI_TOOLKIT_CLASS_STYLE_MENU_BUTTON)
            .ToList();

        Controller.Buttons[0].AddToClassList
            (Constants.UI_TOOLKIT_CLASS_STYLE_ACTIVE_BUTTON);
    }

    public override void SelectButton()
    {
        Button currentButton = Controller.Buttons[Controller.CurrentSelection];

        if (currentButton.name == "start-button")
        {
            SceneTransition.Initiate(1);
        }
    }
}
