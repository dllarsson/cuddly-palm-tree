using UnityEngine;

namespace Utility
{
    public static class Extensions
    {
        // - Rect Extensions - \\

        /// <summary>Creates a new rect with added values from current rect and returns it.</summary>
        public static Rect Add(this Rect rect, float x = 0, float y = 0, float width = 0, float height = 0)
        {
            return new Rect(rect.x + x, rect.y + y, rect.width + width, rect.height + height);
        }

        /// <summary>Adds to rect and passes the modifed rect by reference.</summary>
        /// <remarks>
        /// <c>rectObj.AddRef(out rectObj, width: 100);</c> - modifiedRect parameter can be the same as the original rect.<br/>
        /// <c>rectObj.AddRef(out otherRect, width: 100);</c> - otherRect will be created inside the method and passed as a reference.
        /// </remarks>
        public static Rect Add(this Rect rect, out Rect modifiedRect, float x = 0, float y = 0, float width = 0, float height = 0)
        {
            modifiedRect = new Rect(rect.x + x, rect.y + y, rect.width + width, rect.height + height);
            return modifiedRect;
        }

        /// <summary>Creates a new rect with replaced values from current rect, then returns it.</summary>
        public static Rect Replace(this Rect rect, float? x = null, float? y = null, float? width = null, float? height = null)
        {
            return new Rect(x ?? rect.x, y ?? rect.y, width ?? rect.width, height ?? rect.height);
        }

        /// <summary>Replaces rect values and passes the modifed rect by reference.</summary>
        /// <remarks>
        /// <c>rectObj.AddRef(out rectObj, width: 100);</c> - modifiedRect parameter can be the same as the original rect.<br/>
        /// <c>rectObj.AddRef(out otherRect, width: 100);</c> - otherRect will be created inside the method and passed as a reference.
        /// </remarks>
        public static Rect Replace(this Rect rect, out Rect modifiedRect, float? x = null, float? y = null, float? width = null, float? height = null)
        {
            modifiedRect = new Rect(x ?? rect.x, y ?? rect.y, width ?? rect.width, height ?? rect.height);
            return modifiedRect;
        }
    }
}