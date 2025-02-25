using UI;

namespace DefaultNamespace
{
    public abstract class UIBaseState
    {
        public UIController ControllerCmp;

        public UIBaseState( UIController controller )
        {
            ControllerCmp = controller;
        }

        public abstract void EnterState();

        public abstract void SelectButton();
    }
}