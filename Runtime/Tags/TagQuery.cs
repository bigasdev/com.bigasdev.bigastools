using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace BigasTools{
    public static class TagQuery
    {
        private static Dictionary<string, GameObject> cachedObjects = new Dictionary<string, GameObject>();

        /// <summary>
        /// Find a object by the tag name and cache it.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindObject(string name){
            GameObject obj;
            if(cachedObjects.TryGetValue(name, out obj)){
                return obj;
            }else{
                var o = Object.FindObjectsOfType<Tag>();
                foreach(var g in o){
                    cachedObjects.Add(g.tagName, g.gameObject);
                    if(g.tagName == name)return g.gameObject;
                }
            }
            return null;
        }
        /// <summary>
        /// Dispose all the cached data, useful for reloadings.
        /// </summary>
        public static void DisposeCache(){
            cachedObjects.Clear();
        }
    }
}
