using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);
            var tagList = new List<string>(){ "<All Tags>" };
            tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

            int index = tagList.FindIndex(a => a.Contains(property.stringValue));
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