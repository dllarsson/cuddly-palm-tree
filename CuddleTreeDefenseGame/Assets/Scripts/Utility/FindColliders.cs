using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public static class FindColliders
    {
        //OBSOLETE
        public static Collider2D FindNearestTarget(GameObject originPosition, string tag, float minTurretRange, float maxTurretRange)
        {
            Collider2D nearestTarget = null;
            float? nearest = Mathf.Infinity;
            foreach(var target in Physics2D.OverlapCircleAll(originPosition.transform.position, maxTurretRange))
            {
                if(target.gameObject.CompareTag(tag))
                {
                    float? inRange = InRange(originPosition, target.gameObject, minTurretRange, maxTurretRange);
                    if(inRange != null && inRange < nearest)
                    {
                        nearest  = inRange;
                        nearestTarget = target;
                    }
                }
            }
            return nearestTarget;
        }
        //***

        //Checks if a collider is within a radius of another collider based on tag
        public static List<Collider2D> GetTargets(GameObject originPosition, string tag, float minRange, float maxRange)
        {
            var colliderList = new List<Collider2D>();
            foreach(var collider in Physics2D.OverlapCircleAll(originPosition.transform.position, maxRange))
            {
                if(collider.gameObject.CompareTag(tag))
                {
                    colliderList.Add(collider);
                }
            }
            return (colliderList.Any()) ? colliderList: null;
        }
        //Same as above, but with an array of tags
        public static List<Collider2D> GetTargets(GameObject originPosition, string[] tags, float minRange, float maxRange)
        {
            var colliderList = new List<Collider2D>();
            foreach(string tag in tags)
            {
                var targetList = GetTargets(originPosition, tag, minRange, maxRange);
                if(targetList != null)
                {
                    colliderList.AddRange(targetList);
                }
            }
            return (colliderList.Any()) ? colliderList : null;
        }

        //Uses a list of colliders and returns the closest one to the originPosition
        public static Collider2D GetNearestTarget(GameObject originPosition, List<Collider2D> targetList, float minRange, float maxRange)
        {
            Collider2D nearestTarget = null;
            float? nearest = Mathf.Infinity;
            foreach(var target in targetList)
            {
                float? inRange = InRange(originPosition, target.gameObject, minRange, maxRange);
                if(inRange != null && inRange < nearest)
                {
                    nearest = inRange;
                    nearestTarget = target;
                }
            }
            return nearestTarget;
        }
        //Checks if a gameobject is within a min and max range.
        public static float? InRange(GameObject originObject, GameObject targetObject, float minRange, float maxRange)
        {
            if(originObject != null && targetObject != null)
            {
                float? range = Vector2.Distance(targetObject.transform.position, originObject.transform.position);
                return (range < maxRange && range > minRange) ? range : null;
            }
            return null;
        }
    }
}