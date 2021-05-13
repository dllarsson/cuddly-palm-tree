namespace StateMachine
{
    public interface IState
    {
        public void Update();
        public void Start();
        public void End();
    }
}