using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using BigasTools.UI;

namespace BigasTools.Editor{
    public class DrawerMenu : EditorWindow
    {
        static DrawerOption[] options;
        public static void ShowWindow(DrawerOption[] _keys) {
            var window = GetWindow<DrawerMenu>();
            window.titleContent = new GUIContent("Drawer menu");
            window.Show();
            window.minSize = new Vector2(400,180);
            options = _keys;
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            for (int i = 0; i < options.Length; i++)
            {
                options[i].Refresh(i);
                if(GUILayout.Button(options[i].name, EditorStyles.toolbarButton)){
                    options[i].onGUI();
                    options[i].Update();
                };
                GUILayout.FlexibleSpace();
            }

        }
    }
    [System.Serializable]
    public abstract class DrawerOption{
        public string name {set;get;}

        public abstract void onGUI();
        public abstract void Refresh(int i);
        public abstract void Update();
    }
    [System.Serializable]
    public class DrawerOption<T> : DrawerOption{
        private readonly Func<T[]> getValues;
        private readonly Action<T> setValue;
        private readonly SerializedProperty serializedProperty;
        private T[] values;
        private T value;

        public DrawerOption(Func<T[]> getValues, string name, Action<T> setValue, SerializedProperty serializedProperty)
        {
            this.name = name;
            this.getValues = getValues;
            this.setValue = setValue;
            this.serializedProperty = serializedProperty;
        }

        public override void onGUI()
        {
            if(setValue != null){
                onValueGUI(value);
                //setValue(value);
            }
        }
        public override void Update()
        {
            serializedProperty.enumValueIndex = 14;
            Debug.Log(serializedProperty.serializedObject.ApplyModifiedProperties());
        }
        void onValueGUI(T val){
            setValue(val);
        }
        public override void Refresh(int i){
            values = getValues();
            value = values[i];
        }
    }
}
