using System;
using UnityEngine;
using UnityEditor;
using Utility;

[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
public class MinMaxRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var value = attribute as MinMaxRangeAttribute;

        if(property.propertyType == SerializedPropertyType.Vector2)
        {
            position = position.Replace(height: EditorGUIUtility.singleLineHeight);

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
            float fieldWidth = Math.Max(50f, style.CalcSize(new GUIContent(minValue.ToString())).x + 10);

            var leftRect = position.Replace(EditorGUIUtility.labelWidth - indentValue + 20f, width: fieldWidth);
            vector2Value.x = EditorGUI.FloatField(leftRect, minValue);

            var rightRect = position.Replace((position.x - indentValue) + position.width - fieldWidth, width: fieldWidth);
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