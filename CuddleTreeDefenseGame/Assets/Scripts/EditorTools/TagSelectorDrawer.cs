using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorPropertyDrawer : PropertyDrawer
{
    public int GetElementIndexFromElementList(string find, List<string> list)
    {
        int index = 0;
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] == find)
            {
                index = i;
                break;
            }
        }

        return index;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);
            var tagList = new List<string>(){ "<All Tags>" };
            tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
            
            int index = GetElementIndexFromElementList(property.stringValue, tagList);
            index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());
            property.stringValue = index >= 1 ? tagList[index] : "";

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}