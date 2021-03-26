using UnityEngine;

public class Turrets : MonoBehaviour
{
    private EnemyDragon target;
    [SerializeField] public float RotationSpeed = 20;

    private void OnEnable()
    {
        target = FindObjectOfType<EnemyDragon>();
    }

    void rotateToTarget()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90; // we need to apply a offset since we want the tip of the cannon to point at the target.

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotationSpeed);
    }

    void scanForTarget()
    {
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * RotationSpeed);
    }
    void Update()
    {
        Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);

        float inRange = Vector2.Distance(targetPos, myPos);
        if (inRange < 10.0f )
        {
            rotateToTarget();
        }
        else
        {
            scanForTarget();
        }
    }
}
