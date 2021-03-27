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
}
