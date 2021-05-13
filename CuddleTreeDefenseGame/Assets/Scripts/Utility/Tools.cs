using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Utility
{
    public static class Tools
    {
        // ignore z value
        public static Vector3 GetScreenToWorldPoint2D(Vector3 vector3)
        {
            Vector3 toReturn = new Vector3(0, 0, 0);
            if(vector3 != null)
            {
                toReturn = new Vector3(vector3.x, vector3.y, 0);
            }

            return toReturn;
        }

        public static void SetColorOnGameObject(GameObject src, Color color)
        {
            SpriteRenderer[] children = src.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer comp in children)
            {
                comp.color = color;
            }
        }

        public static void ToggleScriptsInGameObject(GameObject src, bool enable)
        {
            MonoBehaviour[] childScripts = src.GetComponentsInChildren<MonoBehaviour>();
            foreach(MonoBehaviour test in childScripts)
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

        // - String methods - \\

        // - Crates a space between camelCase words:
        // - Example: 'this Text' from 'thisText' - \\
        public static string SpaceBeforeUppercase(string word)
        {
            return string.IsNullOrEmpty(word) ? null : Regex.Replace(word, "([A-Z])", " $1").Trim();
        }
        // - Makes first letter in a string uppercase:
        // - Example: 'Hello' from 'hello'
        public static string UpperCaseFirstLetter(string word)
        {
            return string.IsNullOrEmpty(word) ? null : word.First().ToString().ToUpper() + word.Substring(1);
        }
        // - Combines above two methods:
        // - Example 'Hello There' from 'helloThere'
        public static string WordsUpperCase(string word)
        {
            return string.IsNullOrEmpty(word) ? null : UpperCaseFirstLetter(SpaceBeforeUppercase(Regex.Replace(word, "(.*)[ ]", "").Trim()));
        }
    }
}