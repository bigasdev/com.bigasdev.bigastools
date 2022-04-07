using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace BigasTools{
    public class CreateViewEditor : EditorWindow
    {
        [MenuItem("BigasTools/Create View")]
        private static void ShowWindow() {
            var window = GetWindow<CreateViewEditor>();
            window.titleContent = new GUIContent("View creator");
            window.Show();
            window.minSize = new Vector2(400,180);
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("MODAL DOS ANOES", EditorStyles.wordWrappedLabel);
            GUILayout.Space(70);
            if (GUILayout.Button("CAPITAO DE AREIA")) this.Close();
        }
    }
}
