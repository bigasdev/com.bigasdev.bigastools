using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BigasTools{
    public class ObjectPool : MonoBehaviour
    {
        public string objectId;
        public GameObject objectPrefab;
        private Queue<GameObject> avaliableObjects = new Queue<GameObject>();
        [SerializeField] int maxPlayerCapacity;
        public virtual void Start() {
            GrowPool();
            PoolsManager.Instance.AddToPool(this);
        }
        /// <summary>
        /// Get the object from the pool with a position as base parameter.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public virtual GameObject GetFromPool(Vector3 pos){
            if(avaliableObjects.Count == 0){
                GrowPool();
            }
            var instance = avaliableObjects.Dequeue();
            instance.SetActive(true);
            OnGet(instance);
            instance.transform.position = pos;
            return instance;
        }
        /// <summary>
        /// Use this function to add stuff for when you get a object.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnGet(GameObject obj){
            //Do your stuff...
        }
        /// <summary>
        /// This function will be used to add more objects if needed
        /// </summary>
        public virtual void GrowPool()
        {
            for(int i = 0; i < maxPlayerCapacity; i++){
                var instanceToAdd = Instantiate(objectPrefab);
                instanceToAdd.transform.SetParent(transform);
                AddToPool(instanceToAdd);
            }
        }
        /// <summary>
        /// Use this function to "destroy" the object
        /// </summary>
        /// <param name="instance"></param>
        public virtual void AddToPool(GameObject instance){
            instance.SetActive(false);
            avaliableObjects.Enqueue(instance);
        }
    }
}
