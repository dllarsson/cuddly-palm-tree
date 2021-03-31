using UnityEngine;
using System.Collections;

public class Turrets : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform fireOrigin;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float maxTurretRange = 10f; //Collider radius
    [SerializeField] float maxRotationSpeed = 100f;

    float minTurretRange;
    bool isFiring = false;
    Coroutine fireTurret;
    bool isRotating = false;

    private void Start()
    {
        minTurretRange = GetComponent<SpriteRenderer>().bounds.extents.y * 2;
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
    private void StartFiring()
    {
        if(!isFiring && !isRotating)
        {
            isFiring = true;
            fireTurret = StartCoroutine(FireWithDelay());
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

    IEnumerator FireWithDelay()
    {
        while(true)
        {
            var projectile = Instantiate(projectilePrefab, fireOrigin.position, fireOrigin.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = fireOrigin.up * projectileSpeed;
            Destroy(projectile, 10f);
            yield return new WaitForSeconds(fireRate);
        }
    }
}