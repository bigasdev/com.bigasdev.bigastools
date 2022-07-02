using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace BigasTools.Editor{
    public class CreateViewEditor : EditorWindow
    {
        string viewName;
        bool hasModal;
        Object test;
        List<CreateViewObject> components = new List<CreateViewObject>();
        [MenuItem("Bigas-Tools/Create View &v")]
        private static void ShowWindow() {
            var window = GetWindow<CreateViewEditor>();
            window.titleContent = new GUIContent("View creator");
            window.Show();
            window.minSize = new Vector2(400,180);
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Welcome to the view creator! Here you can create a perfect UI for your case", EditorStyles.wordWrappedLabel);
            GUILayout.Space(70);
            viewName = EditorGUILayout.TextField("View name", viewName);
            hasModal = EditorGUILayout.Toggle("Has modal", hasModal);
            if(GUILayout.Button("Add a child")){
                OnComponentAdd();
            }
            foreach (var item in components)
            {
                string name = "";
                bool isModal = false;
                Object sprite = null;
    
                name = EditorGUILayout.TextField("Child Name", name);
                isModal = EditorGUILayout.Toggle("Is Modal Object", isModal);
                sprite = EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), true);
                if(GUILayout.Button("Remove component")){
                    components.Remove(item);
                }
            }
        }
        void OnComponentAdd(){
            var c = new CreateViewObject();
            components.Add(c);
        }
    }
    [System.Serializable]
    public class CreateViewObject{

    }
}
