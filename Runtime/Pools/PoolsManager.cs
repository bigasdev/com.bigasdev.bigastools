using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigasTools{
    public class PoolsManager : MonoBehaviour
    {
        private static PoolsManager instance;
        public static PoolsManager Instance{
            get{
                if(instance == null)instance = FindObjectOfType<PoolsManager>();
                return instance;
            }
        }
        Dictionary<string, ObjectPool> cachedPools = new Dictionary<string, ObjectPool>();

        /// <summary>
        /// Add a pool to the cache.
        /// </summary>
        /// <param name="obj"></param>
        public void AddToPool(ObjectPool obj){
            cachedPools.Add(obj.objectId, obj);
        }
        /// <summary>
        /// Get a pool from the cache
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObjectPool GetPool(string id){
            if(!cachedPools.ContainsKey(id))return null;
            Debug.Log(cachedPools[id]);
            return cachedPools[id];
        }
    }
}
