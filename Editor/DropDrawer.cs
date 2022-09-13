using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.Rpg;

using UnityEditor;
namespace BigasTools.Editor{
    [CustomPropertyDrawer(typeof(Drop))]
    [CanEditMultipleObjects]
    public class DropDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var item = new Rect(position.x, position.y, position.width, position.height);
            var itemName = new Rect(position.x, position.y + 50, 50, 20);

            EditorGUI.PropertyField(item, property.FindPropertyRelative("item"), GUIContent.none);

            var p = property.FindPropertyRelative("item");
            var data = p.objectReferenceValue as ScriptableObject;
            if(data!=null){
                SerializedObject obj = new SerializedObject(data);
                var name = obj.FindProperty("itemName");
                EditorGUI.LabelField(itemName, name.stringValue);
            }
        }
    }
}
