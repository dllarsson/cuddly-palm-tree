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
        var count = parent.transform.childCount;
        var children = new GameObject[count];
        for(var i = 0; i < count; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    public static Collider2D FindNearestTarget(Vector2 originPosition, string tag, float minTurretRange, float maxTurretRange)
    {
        var nearest = Mathf.Infinity;
        Collider2D nearestTarget = null;
        foreach(var target in Physics2D.OverlapCircleAll(originPosition, maxTurretRange))
        {
            if(target.gameObject.CompareTag(tag))
            {
                var inRange = Vector2.Distance(target.transform.position, originPosition);
                if(inRange < nearest && inRange > minTurretRange)
                {
                    nearest = inRange;
                    nearestTarget = target;
                }
            }
        }
        return nearestTarget;
    }
}
