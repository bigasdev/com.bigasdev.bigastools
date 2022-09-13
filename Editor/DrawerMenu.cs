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
        int tab;
        Vector2 scrollPos;   
        static Drawer[] drawers;
        static DrawerOption[] options;
        public static void ShowWindow(Drawer[] drawer, DrawerOption[] _keys) {
            var window = GetWindow<DrawerMenu>();
            window.titleContent = new GUIContent("Drawer menu");
            window.Show();
            window.minSize = new Vector2(400,180);
            options = _keys;
            drawers = drawer;
        }

        void OnGUI()
        {
            var s = new string[drawers.Length];
            for (int i = 0; i < drawers.Length; i++)
            {
                s[i] = drawers[i].name;
            }
            tab = GUILayout.Toolbar(tab, s);
            switch(tab){
                case 0:
                    GUILayout.BeginVertical(EditorStyles.toolbar);
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(400), GUILayout.Height(500));
                    for (int i = 0; i < options.Length; i++)
                    {
                        options[i].Refresh(i);
                        if(GUILayout.Button(options[i].name, EditorStyles.toolbarButton)){
                            options[i].onGUI();
                            options[i].Update();
                        };
                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndVertical();
                    GUILayout.EndScrollView();
                    break;
                case 1:
                    Debug.Log("Test");
                    break;
            }   
        }
    }
    [System.Serializable]
    public class Drawer{
        public Drawer(string name)
        {
            this.name = name;
        }

        public string name {set;get;}
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
        private int selected;
        KeyCode _selectedKey;

        public DrawerOption(Func<T[]> getValues, string name, Action<T> setValue, SerializedProperty serializedProperty, KeyCode _selectedKey)
        {
            this.name = name;
            this.getValues = getValues;
            this.setValue = setValue;
            this.serializedProperty = serializedProperty;
            this._selectedKey = _selectedKey;
        }

        public override void onGUI()
        {
            if(setValue != null){
                onValueGUI(value);
                //setValue(value);
            }
        }
        void GetEnumIndex(){
            string _selectedKeyName = Enum.GetName(typeof(KeyCode), _selectedKey);
            var keyCodes = Enum.GetValues(typeof(KeyCode));

            int index = 0;

            // Find the index of the selected key name.
            foreach (string enumName in serializedProperty.enumNames) {

                if (enumName == _selectedKeyName) {
                    break;
                }

                index++;
            }

            serializedProperty.enumValueIndex = index;
            serializedProperty.serializedObject.ApplyModifiedProperties();
        }
        public override void Update()
        {
            GetEnumIndex();
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
