using System;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using Object = UnityEngine.Object;

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

        // - String methods - \\
        public static string CamelCaseSpaces(string word)
        {
            return string.IsNullOrEmpty(word) ? null : Regex.Replace(word, "([A-Z])", " $1").Trim();
        }
        public static string UpperCaseFirstLetter(string word)
        {
            return string.IsNullOrEmpty(word) ? null : word.First().ToString().ToUpper() + word.Substring(1);
        }
        public static string UpperSecondWord(string word)
        {
            return string.IsNullOrEmpty(word) ? null : UpperCaseFirstLetter(CamelCaseSpaces(Regex.Replace(word, "(.*)[ ]", "").Trim()));
        }

        // - Reflection methods - \\
        // Don't use in Update() or Coroutines...

        public static Type GetTypeFromString(string type)
        {
            if(string.IsNullOrEmpty(type))
                return null;
            var assem = Assembly.GetExecutingAssembly();
            return assem.GetType(type);
        }
        public static List<Type> GetSubclassScriptsFromType(Type parentScript)
        {
            return (from Type type in Assembly.GetExecutingAssembly().GetTypes()
                where type.IsSubclassOf(parentScript) select type)?.ToList();
        }

        // - Serialized Property methods - \\

        public static bool IsObjectReferenceNotNull(SerializedProperty property)
        {
            if(property?.propertyType == SerializedPropertyType.ObjectReference)
            {
                if(property.objectReferenceValue != null)
                {
                    return true;
                }
            }
            return false;
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
        public static List<string> GetObjectFields(Object obj)
        {
            var strList = new List<string>();

            foreach(var value in obj.GetType().GetFields())
            {
                strList.Add(value.Name);
            }
            return strList;
        }

        public static IEnumerable<SerializedProperty> GetSerializedField(SerializedProperty serializedProperty)
        {
            if(serializedProperty.NextVisible(true))
            {
                do
                {
                    yield return serializedProperty;
                }
                while(serializedProperty.NextVisible(false));
            }
        }
    }
}