using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

using BigasTools.InputSystem;

[CustomPropertyDrawer(typeof(InputProfile))]
public class InputProfileDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var title = new Rect(position.x, position.y, position.width - 50, position.height);
        var name = new Rect(position.x, position.y, 75, position.height);
        var key = new Rect(position.x + 85, position.y, position.width, position.height);
        var joyKey = new Rect(position.x + 170, position.y, 75, position.height);

        //EditorGUI.PrefixLabel(title, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUI.PropertyField(name, property.FindPropertyRelative("inputName"), GUIContent.none);
        EditorGUI.PropertyField(key, property.FindPropertyRelative("inputKey"), GUIContent.none);
        EditorGUI.PropertyField(joyKey, property.FindPropertyRelative("joystickKey"), GUIContent.none);
        

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
