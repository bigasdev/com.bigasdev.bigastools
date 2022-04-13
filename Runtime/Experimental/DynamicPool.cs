using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//
// This class is responsible for a dynamic pool, its a concept where you can spawn any object you want and save it in the dictionary
//
namespace BigasTools{
    public class DynamicPool : MonoBehaviour
    {
        private static DynamicPool instance;
        public static DynamicPool Instance{
            get{
                if(instance == null)instance = FindObjectOfType<DynamicPool>();
                return instance;
            }
        }
        Dictionary<string, List<GameObject>> cachedObjects = new Dictionary<string, List<GameObject>>();

        //
        // Here you need to create your logic to load the object from the resources folder
        //
        public virtual GameObject GetObj(string args){
            return Resources.Load<GameObject>("Prefabs/Particles/" + args);
        }
        //
        // The core for the dynamic pool, here we'll try to get the gameobject inside the list from the cache.
        //
        public virtual GameObject GetFromPool(string args, Vector3 pos){
            for (int i = 0; i < cachedObjects.Count; i++)
            {
                var obj = cachedObjects.ElementAt(i);
                Debug.Log(obj);
                if(obj.Key == args){
                    if(obj.Value.Count <= 0)break;
                    Debug.Log(obj.Value.Count);
                    for (int u = 0; u < obj.Value.Count; u++)
                    {
                        if(!obj.Value[u].activeSelf){
                            var realObj = obj.Value[u];
                            Debug.Log(realObj + "....");
                            realObj.SetActive(true);
                            realObj.transform.position = pos;
                            obj.Value.Remove(realObj);
                            return realObj;
                        }
                    }
                    var prefab = GetObj(args);
                    var prefabIns = Instantiate(prefab, this.transform);
                    prefabIns.transform.position = pos;
                    prefabIns.AddComponent<DynamicChild>().dynamicPoolName = args;
                    return prefabIns;
                }
            }
            var realPrefab = GetObj(args);
            var prefabInstance = Instantiate(realPrefab, this.transform);
            prefabInstance.transform.position = pos;
            prefabInstance.AddComponent<DynamicChild>().dynamicPoolName = args;
            try
            {
                cachedObjects.Add(args, new List<GameObject>(){prefabInstance});
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
            return prefabInstance;
        }
        //
        // The function that will handle the return of an object to the cache (destroying it)
        //
        public virtual void ReturnToPool(string args, GameObject gameObj){
            for (int i = 0; i < cachedObjects.Count; i++)
            {
                var obj = cachedObjects.ElementAt(i);
                if(obj.Key == args){
                    gameObj.SetActive(false);
                    obj.Value.Add(gameObj);
                }
            }
        }
    }
}
