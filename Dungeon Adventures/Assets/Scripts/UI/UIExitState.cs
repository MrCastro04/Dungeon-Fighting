using DefaultNamespace;
using UI;
using UnityEngine.UIElements;

public class UIExitState : UIBaseState
{
    public UIExitState(UIController controller) : base(controller) { }

    public override void EnterState()
    {
        Controller.ExitContainer.style.display = DisplayStyle.Flex;

 
    }

    public override void SelectButton()
    {

    }
}
