using System;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
public class MinMaxRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var value = attribute as MinMaxRangeAttribute;

        if(property.propertyType == SerializedPropertyType.Vector2)
        {
            position = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            float minValue = (float)Math.Round(property.vector2Value.x, 2);
            float maxValue = (float)Math.Round(property.vector2Value.y, 2);
            float minLimit = value.minLimit;
            float maxLimit = value.maxLimit;

            EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, minLimit, maxLimit);
            position.y += EditorGUIUtility.singleLineHeight;

            var vector2Value = Vector2.zero;
            vector2Value.x = minValue;
            vector2Value.y = maxValue;

            var style = new GUIStyle();
            float indentValue = EditorGUI.indentLevel * 15f;
            float width = Math.Max(50f, style.CalcSize(new GUIContent(minValue.ToString())).x + 10);

            var leftRect = new Rect(EditorGUIUtility.labelWidth - indentValue + 20f, position.y, width, position.height);
            vector2Value.x = EditorGUI.FloatField(leftRect, minValue);

            var rightRect = new Rect((position.x - indentValue) + position.width - width, position.y, width, position.height);
            vector2Value.y = EditorGUI.FloatField(rightRect, maxValue);
            position.y += EditorGUIUtility.singleLineHeight;

            property.vector2Value = vector2Value;
        }
        else
        {
            throw new UnityException("Attribute only works for Vector2 properties.");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 2f;
    }
}