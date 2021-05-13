using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;

namespace StateMachine.Behaviours
{
    public class Target_Closest : IState
    {
        private readonly ITargeter obj;
        private readonly Vector2 attackRange;
        private readonly float changeTargetDelay;
        private CancellationTokenSource scanTokenSource;

        public Target_Closest(ITargeter obj, Vector2 attackRange, float changeTargetDelay)
        {
            this.obj = obj;
            this.attackRange = attackRange;
            this.changeTargetDelay = changeTargetDelay;
        }

        public void Start()
        {
            Debug.Log($"{obj}: OnStart");
            scanTokenSource = new CancellationTokenSource();
            GetClosestTarget(scanTokenSource.Token);
        }

        public void Update()
        {
            float? inRange = FindColliders.InRange(obj.CurrentGameObject, obj.TargetObject, attackRange);

            if(inRange == null)
            {
                obj.TargetObject = null;

                //Remove this if we want a delay after target is lost or dead before scanning
                scanTokenSource?.Cancel();
            }
        }
        private async void GetClosestTarget(CancellationToken ct)
        {
            obj.TargetObject = FindColliders.GetNearestTarget(obj.CurrentGameObject, obj.AvaliableTargets, attackRange);
            await UniTask.Delay(TimeSpan.FromSeconds(changeTargetDelay), cancellationToken: ct).SuppressCancellationThrow();
            obj.AvaliableTargets.Clear();
        }
        public void End()
        {
            Debug.Log($"{obj}: OnEnd");
            scanTokenSource?.Cancel();
        }
    }
}