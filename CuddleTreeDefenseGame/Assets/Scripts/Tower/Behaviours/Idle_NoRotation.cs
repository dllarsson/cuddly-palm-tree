using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;

namespace StateMachine.Behaviours
{
    public class Idle_NoRotation : IState
    {
        private readonly ITargeter obj;
        private readonly string[] targetTags;
        private readonly Vector2 attackRange;
        private readonly float scanInterval;

        private CancellationTokenSource scanTokenSource;

        //optional turret?
        // - Require ITargeter interface on base object - \\
        public Idle_NoRotation(ITargeter obj, string[] targetTags, Vector2 attackRange, float scanInterval)
        {
            //test with and without
            if(obj.CurrentGameObject == null)
                throw new ArgumentNullException($"{obj}: Property CurrentGameObject cannot be null.");
            this.obj = obj;
            this.targetTags = targetTags;
            this.attackRange = attackRange;
            this.scanInterval = scanInterval;
        }

        public void Start()
        {
            Debug.Log($"{obj}: OnStart");
            scanTokenSource = new CancellationTokenSource();
            ScanForTargets(scanTokenSource.Token);
        }
        public void Update()
        {
        }
        private async void ScanForTargets(CancellationToken ct)
        {
            while(!ct.IsCancellationRequested)
            {
                obj.AvaliableTargets = FindColliders.GetTargets(obj.CurrentGameObject, targetTags, attackRange);
                if(!obj.AvaliableTargets.Any() && obj.TargetObject != null)
                    obj.TargetObject = null;
                await UniTask.Delay(TimeSpan.FromSeconds(scanInterval), cancellationToken: ct).SuppressCancellationThrow();
            }
        }
        public void End()
        {
            Debug.Log($"{obj}: OnEnd");
            scanTokenSource?.Cancel();
        }
    }
}