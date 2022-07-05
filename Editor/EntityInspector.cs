using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using BigasTools;

[CustomEditor(typeof(Entity))]
public class EntityInspector : Editor
{
    Entity myEntity;
    
    public override void OnInspectorGUI()
    {
        myEntity = (Entity)target;
        //Packages/com.bigasdev.bigastools/Res/logoaseprite400.png
        Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Res/logoaseprite400.png", typeof(Texture));
        var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

        //Packages/com.bigasdev.bigastools/Materials/SpriteBlankMaterial.mat
        Material material = (Material)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Materials/SpriteBlankMaterial.mat", typeof(Material));

        if(myEntity.blinkMaterial == null)myEntity.blinkMaterial = material;

        GUILayout.Box(banner, style);
        if(GUILayout.Button("Check runtime stats")){

        }
        if(GUILayout.Button("Load default stats")){
            myEntity.moveSpeedVariable = 4;
            myEntity.life = 100;
            myEntity.maxLife = 100;
            myEntity.spriteSquashSpeed = 4.5f;
        }
        BuildEverything();
    }
    void BuildEverything(){
        serializedObject.Update();
        var life = serializedObject.FindProperty("lifeVariable");
        var maxLife = serializedObject.FindProperty("maxLifeVariable");
        var squashSpeed = serializedObject.FindProperty("spriteSquashSpeed");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("moveSpeedVariable"), new GUIContent("Move Speed", (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Res/bootsIcon.png", typeof(Texture)),"Change the speed of the entity"));
        EditorGUILayout.PropertyField(life, new GUIContent("Life", (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Res/heartIcon.png", typeof(Texture)),"Change the current health of the entity"));
        EditorGUILayout.PropertyField(maxLife, new GUIContent("Max Life", (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Res/heartIcon.png", typeof(Texture)),"Change the max health of the entity"));
        EditorGUILayout.PropertyField(squashSpeed, new GUIContent("Squash Speed", (Texture)AssetDatabase.LoadAssetAtPath("Packages/com.bigasdev.bigastools/Res/blobsicon.png", typeof(Texture)),"Change the sprite squash speed of the entity"));
        //myEntity.moveSpeedVariable = EditorGUILayout.FloatField("Move Speed", myEntity.moveSpeedVariable);
        serializedObject.ApplyModifiedProperties();
    }
}