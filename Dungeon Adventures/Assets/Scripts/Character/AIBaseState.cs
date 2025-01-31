namespace Character
{
    public abstract class AIBaseState
    {
        public abstract void EnterState(EnemyController controller);

        public abstract void UpdateState(EnemyController controller);
    }
}