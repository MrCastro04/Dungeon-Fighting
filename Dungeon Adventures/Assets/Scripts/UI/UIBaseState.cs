using UI;

namespace DefaultNamespace
{
    public abstract class UIBaseState
    {
        public UIController Controller;

        public UIBaseState( UIController controller )
        {
            Controller = controller;
        }

        public abstract void EnterState();

        public abstract void SelectButton();
    }
}