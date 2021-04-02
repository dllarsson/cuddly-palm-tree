using UnityEngine;

public static class Tools
{
    // ignore z value
    public static Vector3 GetScreenToWorldPoint2D(Vector3 vector3)
    {
        Vector3 toReturn = new Vector3(0, 0, 0);
        if (vector3 != null)
        {
            toReturn = new Vector3(vector3.x, vector3.y, 0);
        }

        return toReturn;
    }

    public static void SetColorOnGameObject(GameObject src, Color color)
    {
        SpriteRenderer[] children = src.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer comp in children)
        {
            comp.color = color;
        }
    }

    public static void ToggleScriptsInGameObject(GameObject src, bool enable)
    {
        MonoBehaviour[] childScripts = src.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour test in childScripts)
        {
            test.enabled = enable;
        }
    }
    public static GameObject[] GetChildren(GameObject parent)
    {
        int count = parent.transform.childCount;
        var children = new GameObject[count];
        for(int i = 0; i < count; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    public static Collider2D FindNearestTarget(GameObject originPosition, string tag, float minTurretRange, float maxTurretRange)
    {
        Collider2D nearestTarget = null;
        foreach(var target in Physics2D.OverlapCircleAll(originPosition.transform.position, maxTurretRange))
        {
            if(target.gameObject.CompareTag(tag))
            {
                float? inRange = IsInRange(originPosition, target.gameObject, minTurretRange, maxTurretRange);
                if(inRange != null)
                {
                    float? nearest = inRange;
                    nearestTarget = target;
                }
            }
        }
        return nearestTarget;
    }
    public static float? IsInRange(GameObject originObject, GameObject targetObject, float minRange, float maxRange)
    {
        if(originObject != null && targetObject != null)
        {
            float? range = Vector2.Distance(targetObject.transform.position, originObject.transform.position);
            return (range < maxRange && range > minRange) ? range : null;
        }
        return null;
    }
}
