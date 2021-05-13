using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;

namespace StateMachine.Behaviours
{
    public class Idle_ScanForTargetsSporatic : IState
    {
        private readonly ITargeter obj;
        private readonly GameObject turret;
        private readonly int rotationDirection;
        private readonly float searchRotationSpeed;
        private readonly string[] targetTags;
        private readonly Vector2 attackRange;
        private readonly float scanInterval;

        private CancellationTokenSource scanTokenSource;
        private Vector3 turnDirection;

        private Vector2 directionChangeInterval = new Vector2(1f, 10f);
        private float timeBeforeDirectionChange = 0f;

        public Idle_ScanForTargetsSporatic(ITargeter obj, GameObject turret, int rotationDirection, float searchRotationSpeed, string[] targetTags, Vector2 attackRange, float scanInterval)
        {
            //test with and without
            if(obj.CurrentGameObject == null)
                throw new ArgumentNullException($"{obj}: Property CurrentGameObject cannot be null.");
            this.obj = obj;
            this.turret = turret;
            this.rotationDirection = rotationDirection;
            this.searchRotationSpeed = searchRotationSpeed;
            this.targetTags = targetTags;
            this.attackRange = attackRange;
            this.scanInterval = scanInterval;
        }

        public void Start()
        {
            Debug.Log($"{obj}: OnStart");
            turnDirection = (rotationDirection == 1) ? Vector3.back : Vector3.forward;
            scanTokenSource = new CancellationTokenSource();
            ScanForTargets(scanTokenSource.Token);
        }
        public void Update()
        {
            if(timeBeforeDirectionChange <= 0)
            {
                turnDirection = (turnDirection == Vector3.back) ? Vector3.forward : Vector3.back;
                timeBeforeDirectionChange = UnityEngine.Random.Range(directionChangeInterval.x, directionChangeInterval.y);
            }
            turret.transform.Rotate(turnDirection, Time.deltaTime * searchRotationSpeed);
            timeBeforeDirectionChange -= Time.deltaTime;
        }
        private async void ScanForTargets(CancellationToken ct)
        {
            while(!ct.IsCancellationRequested)
            {
                obj.AvaliableTargets = FindColliders.GetTargets(obj.CurrentGameObject, targetTags, attackRange);
                if(obj.AvaliableTargets == null && obj.TargetObject != null)
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