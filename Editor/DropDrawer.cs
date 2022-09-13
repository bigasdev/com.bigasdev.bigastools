using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.Rpg;
using BigasMath;

using UnityEditor;
namespace BigasTools.Editor{
    [CustomPropertyDrawer(typeof(Drop))]
    [CanEditMultipleObjects]
    public class DropDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 75;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var title = new Rect(position.x, position.y - 8, 80, 20);
            var item = new Rect(position.x + 52, position.y + 10, 80, 20);
            var chance = new Rect(position.x + 50, position.y + 40, 50, 20);
            var itemName = new Rect(position.x, position.y + 10, 50, 20);
            var itemIcon = new Rect(position.x + 10, position.y + 30, 32, 32);
            var itemChance = new Rect(position.x, position.y + 60, 120, 20);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.LabelField(title, "DROP!");
    

            EditorGUI.PropertyField(item, property.FindPropertyRelative("item"), GUIContent.none);
            EditorGUI.PropertyField(chance, property.FindPropertyRelative("chance"), GUIContent.none);

            var c = property.FindPropertyRelative("chance").intValue;
            if(c!=0){
                EditorGUI.LabelField(itemChance, $"{BMathPercentage.GetPercentageFromFloat(1, c)}% (1/{c})");
            }   

            EditorGUI.DrawRect(itemName, Color.red);


            var p = property.FindPropertyRelative("item");
            var data = p.objectReferenceValue as ScriptableObject;
            if(data!=null){
                SerializedObject obj = new SerializedObject(data); 
                var name = obj.FindProperty("itemName");
                var texture = obj.FindProperty("sprite");
                EditorGUI.LabelField(itemName, name.stringValue);
                var a = AssetPreview.GetAssetPreview(texture.objectReferenceValue);
                if(a!=null){
                    EditorGUI.DrawPreviewTexture(itemIcon, AssetPreview.GetAssetPreview(texture.objectReferenceValue));
                }
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
