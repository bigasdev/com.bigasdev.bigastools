using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BigasTools;

namespace BigasTools.Editor{
    public class TagCreatorEditor : EditorWindow {
        string tagName;
        static GameObject obj;
        static Tags tags;

        /*[MenuItem("Bigas-Tools/Tag Creator #t")]
        private static void ShowWindow() {
            var window = GetWindow<TagCreatorEditor>();
            window.titleContent = new GUIContent("TagCreator");
            window.maxSize = new Vector2(400,120);
            window.Show();
        }

        private void OnGUI() {
            
        }*/
        [MenuItem("GameObject/Add Tag")]
        static void AddTag(MenuCommand menuCommand){
            var go = menuCommand.context as GameObject;
            if(go!=null)obj = go;
            var window = GetWindow<TagCreatorEditor>();
            window.titleContent = new GUIContent("TagCreator");
            window.maxSize = new Vector2(400,80);
            window.Show();
        }
        void OnGUI(){
            EditorGUILayout.LabelField("Select the tag type and write a name for it", EditorStyles.wordWrappedLabel);
            tags = (Tags)EditorGUILayout.EnumPopup("Tag Type", tags);
            tagName = EditorGUILayout.TextField("Tag Name", tagName);
            if(GUILayout.Button("Add Tag")){
                OnCreate();
            }
        }
        void OnCreate(){
            obj.AddComponent<Tag>().tagName = tagName;
            obj.GetComponent<Tag>().tagRef = tags;
            this.Close();
        }
    }
}
