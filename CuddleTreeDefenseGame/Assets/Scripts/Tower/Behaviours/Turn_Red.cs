using UnityEngine;

namespace StateMachine.Behaviours
{
    public class Turn_Red : IState
    {
        private readonly ITargeter obj;
        public Turn_Red(ITargeter obj)
        {
            this.obj = obj;
        }

        public void End() => Utility.Tools.SetColorOnGameObject(obj.CurrentGameObject, Color.white);
        public void Start() => Utility.Tools.SetColorOnGameObject(obj.CurrentGameObject, Color.red);
        public void Update()
        {
        }
    }
}