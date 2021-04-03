using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject muzzleFlashPrefab;
    [Header("Settings")]
    [SerializeField] Transform fireOrigin;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float maxTurretRange = 10f; //Collider radius
    [SerializeField] float maxRotationSpeed = 100f;
    [Header("Advanced Settings")]
    [SerializeField] float scanInterval = 0.5f;

    float minTurretRange;
    bool isFiring = false;
    bool isRotating = false;
    bool isReloaded = true;
    bool isScanning = false;
    Coroutine fireRoutine;
    Coroutine scanRoutine;
    GameObject target = null;

    public float MaxTurretRange { get => maxTurretRange; set => maxTurretRange = value; }

    private void Start()
    {
        minTurretRange = GetComponent<SpriteRenderer>().bounds.extents.y * 2;
        StartCoroutine(PeriodicallyScanForTarget());
    }
    IEnumerator PeriodicallyScanForTarget()
    {
        if(!isScanning)
        {
            isScanning = true;
            scanRoutine = StartCoroutine(ScanForTarget());
        }
        while(target == null)
        {
            var nearestTarget = Tools.FindNearestTarget(gameObject, "Enemy", minTurretRange, MaxTurretRange);
            if(nearestTarget != null)
            {
                target = nearestTarget.gameObject;
                StopCoroutine(scanRoutine);
                isScanning = false;
                StartCoroutine(LockOnTarget());
                yield break;
            }
            yield return new WaitForSeconds(scanInterval);
        }
    }
    IEnumerator ScanForTarget()
    {
        while(true)
        {
            transform.Rotate(Vector3.back, Time.deltaTime * maxRotationSpeed / 5);
            yield return null;
        }
    }
    IEnumerator LockOnTarget()
    {
        while(true)
        {
            if(Tools.IsInRange(gameObject, target, minTurretRange, MaxTurretRange) != null
                && target != null)
            {
                var targetAcquired = StartCoroutine(RotateToTarget(target));
                yield return targetAcquired;
                StartFiring();
            }
            else
            {
                target = null;
                StopFiring();
                StartCoroutine(PeriodicallyScanForTarget());
                yield break;
            }
        }
    }
    IEnumerator RotateToTarget(GameObject target)
    {
        isRotating = true;
        var startRotation = transform.rotation;
        var endRotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 0) * (target.transform.position - transform.position).normalized);
        float angle = Quaternion.Angle(startRotation, endRotation);
        float speed = angle / maxRotationSpeed;
        float time = 0f;
        while(time < speed)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / speed);
            yield return null;
            time += Time.deltaTime;
        }
        transform.rotation = endRotation;
        isRotating = false;
    }

    private void StartFiring()
    {
        if(!isFiring)
        {
            isFiring = true;
            fireRoutine = StartCoroutine(ContinousFire());
        }
    }
    private void StopFiring()
    {
        if(isFiring)
        {
            isFiring = false;
            StopCoroutine(fireRoutine);
        }
    }
    IEnumerator ContinousFire()
    {
        while(true)
        {
            yield return new WaitWhile(() => isReloaded == false);
            yield return new WaitWhile(() => isRotating == true);
            if(!isFiring) yield break;
            var projectile = Instantiate(projectilePrefab, fireOrigin.position, fireOrigin.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = fireOrigin.up * projectileSpeed;
            FireProjectileEffect();
            Destroy(projectile, 10f);
            StartCoroutine(ReloadTime());
        }
    }
    IEnumerator ReloadTime()
    {
        isReloaded = false;
        yield return new WaitForSeconds(fireRate);
        isReloaded = true;
    }
    private void FireProjectileEffect()
    {
        var muzzleFlash = Instantiate(muzzleFlashPrefab, fireOrigin.position, fireOrigin.rotation);
        float size = Random.Range(0.3f, 0.5f);
        muzzleFlash.transform.localScale = new Vector3(size, size, size);
        Destroy(muzzleFlash, 1.0f);
    }
    private void OnDestroy()
    {
        //cleanup
        StopAllCoroutines();
    }
}