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
            string propertyString = property.stringValue;
            int index = -1;
            if(propertyString == "")
            {
                index = 0;
            }
            else
            {
                for(int i = 1; i < tagList.Count; i++)
                {
                    if(tagList[i] == propertyString)
                    {
                        index = i;
                        break;
                    }
                }
            }

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