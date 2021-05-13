using UnityEngine;
using Utility;

namespace StateMachine.Behaviours
{
    public class Target_Default : IState
    {
        private readonly ITargeter obj;
        private readonly Vector2 attackRange;

        public Target_Default(ITargeter obj, Vector2 attackRange, float changeTargetDelay)
        {
            this.obj = obj;
            this.attackRange = attackRange;
        }

        public void Start()
        {
            GetClosestTarget();
        }

        public void Update()
        {
            float? inRange = FindColliders.InRange(obj.CurrentGameObject, obj.TargetObject, attackRange);
            if(inRange == null)
            {
                obj.TargetObject = null;
                obj.AvaliableTargets.Clear();
            }
        }
        private void GetClosestTarget()
        {
            obj.TargetObject = FindColliders.GetNearestTarget(obj.CurrentGameObject, obj.AvaliableTargets, attackRange);
        }
        public void End()
        {
        }
    }
}