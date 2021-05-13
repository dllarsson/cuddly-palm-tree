using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public static class FindColliders
    {
        //OBSOLETE - use GetTargets instead
        public static Collider2D FindNearestTarget(GameObject originPosition, string tag, Vector2 minMaxRange)
        {
            Collider2D nearestTarget = null;
            float? nearest = Mathf.Infinity;
            foreach(var target in Physics2D.OverlapCircleAll(originPosition.transform.position, minMaxRange.y))
            {
                if(target.gameObject.CompareTag(tag))
                {
                    float? inRange = InRange(originPosition, target.gameObject, minMaxRange);
                    if(inRange != null && inRange < nearest)
                    {
                        nearest = inRange;
                        nearestTarget = target;
                    }
                }
            }
            return nearestTarget;
        }
        //***

        //Checks if a collider is within a radius of another collider based on tag
        public static List<Collider2D> GetTargets(GameObject originPosition, string tag, Vector2 minMaxRange)
        {
            return (Physics2D.OverlapCircleAll(originPosition.transform.position, minMaxRange.y).
                Where(collider => collider.gameObject.CompareTag(tag) && InRange(originPosition, collider.gameObject, minMaxRange) != null)).ToList();
        }
        //Same as above, but with an array of tags
        public static List<Collider2D> GetTargets(GameObject originPosition, string[] tags, Vector2 minMaxRange)
        {
            var colliderList = new List<Collider2D>();
            foreach(string tag in tags)
            { 
                colliderList.AddRange(GetTargets(originPosition, tag, minMaxRange));
            }
            return colliderList;
        }
        //Checks if a gameobject is within a min and max range.
        public static float? InRange(GameObject originObject, GameObject targetObject, in Vector2 minMaxRange)
        {
            if(originObject != null && targetObject != null)
            {
                float? range = Vector2.Distance(targetObject.transform.position, originObject.transform.position);
                return minMaxRange.x <= range && range <= minMaxRange.y ? range : null;
            }
            return null;
        }
        //Uses a list of colliders and returns the closest one to the originPosition
        public static GameObject GetNearestTarget(GameObject originPosition, List<Collider2D> targetList, in Vector2 minMaxRange)
        {
            GameObject nearestTarget = null;
            float? nearest = Mathf.Infinity;
            foreach(var target in targetList)
            {
                float? inRange = InRange(originPosition, target.gameObject, minMaxRange);
                if(inRange != null && inRange < nearest)
                {
                    nearest = inRange;
                    nearestTarget = target.gameObject;
                }
            }
            return nearestTarget;
        }
        public static GameObject GetTargetLowestHealth(List<Collider2D> targetList)
        {
            GameObject lowestTarget = null;
            float lowest = (targetList.First() is IHealthHandler) ?
                targetList.Cast<IHealthHandler>().Max(x => x.MaxHealth) :
                Mathf.Infinity;

            foreach(var target in targetList)
            {
                float? targetHealth = target?.GetComponent<IHealthHandler>()?.Health;
                if(targetHealth != null && lowest > targetHealth)
                {
                    lowest = target.GetComponent<IHealthHandler>().Health;
                    lowestTarget = target.gameObject;
                }
            }
            return lowestTarget;
        }

        public static GameObject GetTargetHighestHealth(GameObject originTarget, List<Collider2D> targetList)
        {
            var objectHealthHandler = originTarget?.GetComponent<IHealthHandler>();

            GameObject highestTarget = null;
            float highest = 0f;

            foreach(var target in targetList)
            {
                float? targetHealth = target.GetComponent<IHealthHandler>()?.Health;

                if(targetHealth != null && highest < targetHealth)
                {
                    highest = target.GetComponent<IHealthHandler>().Health;
                    highestTarget = target.gameObject;
                }
            }
            return (objectHealthHandler == null || objectHealthHandler.Health < highest) ? highestTarget : originTarget;
        }
        public static GameObject GetTargetGrouped(GameObject originTarget, List<Collider2D> targetList, float radius)
        {
            GameObject groupTarget = null;
            int groupCount = 0;
            int originCount = 0;
            foreach(var target in targetList)
            {
                int countNearby = 0;
                foreach(var nearbyTarget in targetList)
                    countNearby = InRange(target.gameObject, nearbyTarget.gameObject, new Vector2(0f, radius)) != null ? countNearby + 1 : countNearby;
                if(groupCount <= countNearby)
                {
                    groupCount = countNearby;
                    groupTarget = target.gameObject;
                }
                originCount = target == originTarget ? countNearby : originCount;
            }
            return originCount < groupCount ? groupTarget : originTarget;
        }
    }
}