using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools;
using BigasTools.InputSystem;
public class EntityExample : MonoBehaviour
{
    [SerializeField] Entity entityToSpawn;
    List<Entity> entitysOnExample = new List<Entity>();
    private void Update() {
        if(BGameInput.Instance.GetKeyPress("Interaction")){
            var e = Instantiate(entityToSpawn);
            e.transform.position = new Vector2(Random.Range(-13, 13), Random.Range(-8, 8));
            e.onDispose += () =>{
                entitysOnExample.Remove(e);
            };
            entitysOnExample.Add(e);
        }
        if(BGameInput.Instance.GetKeyPress("Example")){
            var rnd = Random.Range(0, entitysOnExample.Count - 1);

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
    }
}
