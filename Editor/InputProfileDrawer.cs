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
        base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        if(EditorGUI.DropdownButton(position, new GUIContent("Test"), FocusType.Keyboard)){
            
        };

        EditorGUI.PropertyField(new Rect(position.x, position.y + 50, position.width, position.height), property.FindPropertyRelative("inputName"), GUIContent.none);
        

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
