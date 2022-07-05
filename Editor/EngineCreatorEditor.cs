using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using BigasTools.InputSystem;

namespace BigasTools.Editor{
    public class EngineCreatorEditor : EditorWindow
    {
        [MenuItem("Bigas-Tools/Create Engine #b")]
        private static void ShowWindow() {
            var window = GetWindow<EngineCreatorEditor>();
            window.titleContent = new GUIContent("Create Engine");
            window.maxSize = new Vector2(400,180);
            window.Show();
            window.minSize = new Vector2(400,180);
        }

        void OnGUI()
        {
            //Packages/com.bigasdev.bigastools/Res/logoaseprite400.png
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Res/logoaseprite400.png", typeof(Texture));
            var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};
            GUILayout.Box(banner, style);
            EditorGUILayout.LabelField("Welcome to Bigas Tools! With this Editor view you can launch the 'Engine' GameObject in the current scene, this object will contain everything necessary to run with the package.", EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("Create Engine")) {
                var g = new GameObject();
                var audioController = new GameObject();
                var resourceController = new GameObject();
                var cooldownController = new GameObject();
                var stateController = new GameObject();
                var bGameInput = new GameObject();
                audioController.transform.SetParent(g.transform);
                resourceController.transform.SetParent(g.transform);
                stateController.transform.SetParent(g.transform);
                cooldownController.transform.SetParent(g.transform);
                bGameInput.transform.SetParent(g.transform);

                g.name = "Engine";
                audioController.name = "Audio Controller";
                resourceController.name = "Resource Controller";
                cooldownController.name = "Cooldown Controller";
                stateController.name = "State Controller";
                bGameInput.name = "Input";

                audioController.AddComponent<AudioController>();
                resourceController.AddComponent<ResourceController>();
                cooldownController.AddComponent<CooldownController>();
                stateController.AddComponent<StateController>();
                bGameInput.AddComponent<BGameInput>();
                this.Close();
            };
        }
    }
}
