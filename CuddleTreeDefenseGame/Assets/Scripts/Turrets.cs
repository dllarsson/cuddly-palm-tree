using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxTurretRange = 10f; //Collider radius
    float minTurretRange;

    private void Start()
    {
        minTurretRange = GetComponent<SpriteRenderer>().bounds.extents.y * 2;
        //target = FindObjectOfType<EnemyDragon>();
    }

    void RotateToTarget(GameObject target)
    {
        //Vector3 vectorToTarget = target.transform.position - transform.position;
        //float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        //angle -= 90; // we need to apply a offset since we want the tip of the cannon to point at the target.

        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 0) * (target.transform.position - transform.position).normalized);
    }

    void ScanForTarget()
    {
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * rotationSpeed);
    }
    void Update()
    {
        SearchForEnemies();
    }
    private void SearchForEnemies()
    {
        float nearest = Mathf.Infinity;
        Collider2D nearestTarget = null;
        foreach(var target in Physics2D.OverlapCircleAll(transform.position, maxTurretRange))
        {
            if(target.gameObject.CompareTag("Enemy"))
            {
                float inRange = Vector2.Distance(target.transform.position, transform.position);
                if(inRange < nearest && inRange > minTurretRange)
                {
                    nearest = inRange;
                    nearestTarget = target;
                }
            }
        }
        Debug.Log(nearestTarget);
        if(nearestTarget != null && nearestTarget.gameObject.CompareTag("Enemy"))
        {
            RotateToTarget(nearestTarget.gameObject);
        }
        else
        {
            ScanForTarget();
        }
    }
}