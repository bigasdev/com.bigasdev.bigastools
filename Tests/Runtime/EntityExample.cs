using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
using BigasTools.InputSystem;
using BigasTools.Rpg;
public class EntityExample : MonoBehaviour
{
    [SerializeField] Drop[] itemDrop;
    [SerializeField] int roll = 4;
    [SerializeField] Entity entityToSpawn;
    List<Entity> entitysOnExample = new List<Entity>();
    float lootTableTimer = 0;
    Timer timerTest, repeatTimerTest, noActionTimerTest;
    private void Start() {
        CameraManager.Instance.OnTargetChange.AddListener((target)=>{
            Debug.Log(target);
        });
        CameraManager.Instance.OnCameraShake.AddListener((d,m)=>{
            Debug.Log(d+m);
        });

        timerTest = new Timer(2f, false, ()=>{
            Debug.Log("Test!!");
        });

        repeatTimerTest = new Timer(1f, true, ()=>{
            Debug.Log("Repeating");
        });

        noActionTimerTest = new Timer(2f, true);
    }
    private void Update() {
        timerTest.Update();
        repeatTimerTest.Update();
        noActionTimerTest.Update();

        if(BGameInput.Instance.GetKeyPress("Interaction")){
            var e = Instantiate(entityToSpawn);
            e.transform.position = new Vector2(Random.Range(-13, 13), Random.Range(-8, 8));
            e.onDispose += () =>{
                entitysOnExample.Remove(e);
            };
            CameraManager.Instance.SetTarget(e.transform, false, false, $"Changing to {e}");
            entitysOnExample.Add(e);
        }
        if(BGameInput.Instance.GetKeyPress("Example")){
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

            CameraManager.Instance.SetShake(.5f, 1.5f, "Testing the zoom!");

            entitysOnExample[rnd].MoveTo(new Vector2(1,0));
        }
        if(BGameInput.Instance.GetKeyPress("Example1")){
            CooldownController.Instance.CheckCurrentLoops();
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

            entitysOnExample[rnd].squashY = .525f;
        }
        if(BGameInput.Instance.GetKeyPress("Example2")){
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

            entitysOnExample[rnd].Shake(.15f, 0, .25f);
        }
        if(BGameInput.Instance.GetKeyPress("Example3")){
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

            entitysOnExample[rnd].Hit(1);
        }
        if(BGameInput.Instance.GetKeyPress("Example4")){
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

            entitysOnExample[rnd].Talk(new BigasTools.UI.TextData("Hello human!"));
        }
        if(BGameInput.Instance.GetKeyPress("Example5")){
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

            entitysOnExample[rnd].Kill();
        }

        LootTableTest();
    }

    void LootTableTest(){
        lootTableTimer += Time.deltaTime;
        if(lootTableTimer >= 10f){
            foreach(var i in itemDrop){
                LootTable.GetDrop(i, roll);
            }
            lootTableTimer = 0;
        }
    }
}
