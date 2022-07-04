using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using BigasTools.UI;

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
                item.name = EditorGUILayout.TextField("Child Name", item.name);
                item.isModal = EditorGUILayout.Toggle("Is Modal Object", item.isModal);
                item.sprite = EditorGUILayout.ObjectField("Sprite", item.sprite, typeof(Sprite), true);
                if(GUILayout.Button("Remove component")){
                    try
                    {
                        components.Remove(item);
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            if(GUILayout.Button("Create View")){
                var obj = new GameObject();

                obj.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                obj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                obj.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920,1080);
                obj.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
                obj.AddComponent<GraphicRaycaster>();
                obj.name = viewName;

                if(hasModal)obj.AddComponent<ModalHandler>();

                foreach (var item in components)
                {
                    var o = new GameObject();

                    o.name = item.name;
                    if(item.isModal)o.AddComponent<ModalObject>();
                    if(item.sprite!=null){
                        o.AddComponent<Image>().sprite = item.sprite as Sprite;
                    }
                    o.transform.SetParent(obj.transform);
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
        public string name;
        public bool isModal;
        public Object sprite;
    }
}
