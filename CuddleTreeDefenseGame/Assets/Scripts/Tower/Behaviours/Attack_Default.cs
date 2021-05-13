using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
namespace StateMachine.Behaviours
{
    public class Attack_Default : IState
    {
        private readonly ITargeter obj;
        private readonly GameObject turret;
        private readonly GameObject projectilePrefab;
        private readonly GameObject muzzleFlashPrefab;
        private readonly Transform fireOrigin;
        private readonly float maxTurnSpeed;
        private readonly float fireRate;
        private readonly float projectileSpeed;
        private readonly string[] targetTags;

        private bool isReloading = false;

        public Attack_Default(ITargeter obj, GameObject turret, GameObject projectilePrefab, GameObject muzzleFlashPrefab, Transform fireOrigin, float maxTurnSpeed, float fireRate, float projectileSpeed, string[] targetTags)
        {
            this.obj = obj;
            this.turret = turret;
            this.projectilePrefab = projectilePrefab;
            this.muzzleFlashPrefab = muzzleFlashPrefab;
            this.fireOrigin = fireOrigin;
            this.maxTurnSpeed = maxTurnSpeed;
            this.fireRate = fireRate;
            this.projectileSpeed = projectileSpeed;
            this.targetTags = targetTags;
        }
        public void Start()
        {
            Debug.Log($"{obj}: OnStart");
        }
        public void Update()
        {
            if(RotateTowardsPoint(obj.TargetObject.transform))
            {
                if(!isReloading)
                {
                    FireProjectile();
                    Reload();
                }
            }
        }
        //Returns true if duration is 0
        private bool RotateTowardsPoint(Transform target)
        {
            var startRotation = turret.transform.rotation;

            var direction = (target.position - turret.transform.position).normalized;
            var targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            float duration = Quaternion.Angle(startRotation, targetRotation) / maxTurnSpeed;
            turret.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Time.deltaTime / duration);
            return duration == 0;
        }
        private void FireProjectile()
        {
            var projectile = Object.Instantiate(projectilePrefab, fireOrigin.position, fireOrigin.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = fireOrigin.up * projectileSpeed;

            foreach(string tag in targetTags)
            {
                projectile.GetComponent<IDamageDealer>().TargetTag.Add(tag);
            }
            var muzzleFlash = Object.Instantiate(muzzleFlashPrefab, fireOrigin.position, fireOrigin.rotation);
            float size = Random.Range(0.3f, 0.5f);
            muzzleFlash.transform.localScale = new Vector3(size, size, size);
            Object.Destroy(muzzleFlash, 1.0f);
            Object.Destroy(projectile, 10f);
        }
        private async void Reload()
        {
            isReloading = true;
            await UniTask.Delay(TimeSpan.FromSeconds(fireRate));
            isReloading = false;
        }

        public void End()
        {
            Debug.Log($"{obj}: OnEnd");
        }
    }
}