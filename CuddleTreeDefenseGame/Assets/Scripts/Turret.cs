using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject muzzleFlashPrefab;
    [SerializeField] Transform fireOrigin;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float maxTurretRange = 10f; //Collider radius
    [SerializeField] float maxRotationSpeed = 100f;

    float minTurretRange;
    bool isFiring = false;
    bool isRotating = false;
    bool isReloaded = true;
    Coroutine fireTurret;
   

    private void Start()
    {
        minTurretRange = GetComponent<SpriteRenderer>().bounds.extents.y * 2;
    }
    void Update()
    {
        var nearestTarget = Tools.FindNearestTarget(transform.position, "Enemy", minTurretRange, maxTurretRange);
        if(nearestTarget != null && nearestTarget.gameObject.CompareTag("Enemy"))
        {
            RotateToTarget(nearestTarget.gameObject);
            StartFiring();
        }
        else
        {
            StopFiring();
            ScanForTarget();
        }
    }
    void RotateToTarget(GameObject target)
    {
        var startRotation = transform.rotation;
        var endRotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 0) * (target.transform.position - transform.position).normalized);

        if(!isRotating)
        {
            isRotating = true;
            var angle = Quaternion.Angle(startRotation, endRotation);
            StartCoroutine(RotateOverTime(startRotation, endRotation, angle / maxRotationSpeed));
        }
        else
        {
            StopFiring();
        }
    }
    IEnumerator RotateOverTime(Quaternion start, Quaternion end, float speed)
    {
        var time = 0f;
        while(time < speed)
        {
            transform.rotation = Quaternion.Slerp(start, end, time / speed);
            yield return null;
            time += Time.deltaTime;
        }
        transform.rotation = end;
        isRotating = false;
    }
    void ScanForTarget()
    {
        transform.Rotate(Vector3.back, Time.deltaTime * maxRotationSpeed / 5);
    }
    private void StartFiring()
    {
        if(!isFiring && !isRotating)
        {
            isFiring = true;
            fireTurret = StartCoroutine(ContinousFire());
        }
    }
    private void StopFiring()
    {
        if(isFiring)
        {
            isFiring = false;
            StopCoroutine(fireTurret);
        }
    }
    IEnumerator ContinousFire()
    {
        while(true)
        {
            yield return new WaitWhile(() => isReloaded == false);
            var projectile = Instantiate(projectilePrefab, fireOrigin.position, fireOrigin.rotation) as GameObject;
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
        var size = Random.Range(0.3f, 0.5f);
        muzzleFlash.transform.localScale = new Vector3(size, size, size);
        Destroy(muzzleFlash, 1.0f);
    }
    private void OnDestroy()
    {
        //cleanup
        StopAllCoroutines();
    }
}