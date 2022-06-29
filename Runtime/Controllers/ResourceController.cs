using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace BigasTools{
    public class ResourceController: MonoBehaviour
    {
        private static ResourceController instance;
        public static ResourceController Instance{
            get{
                if(instance == null)instance = FindObjectOfType<ResourceController>();
                return instance;
            }
        }
        [SerializeField] ResourceReference[] refs;
        ResourceReference GetRef(string name){
            try
            {
                return refs.Where(x => x.referenceName == name).FirstOrDefault();
            }
            catch (System.Exception)
            {
                BDebug.Log($"No '{name}' reference found", "Resource Controller", Color.red);
                return null;
            }
        }
        public T GetObject<T>(string name, string path = "Default") where T:class{
            var p = GetRef(path);
            if(p == null)return null;
            try
            {
                var fullPath = p.directory + "/" +name;
                BDebug.Log($"Trying to load '{fullPath}'", "Resource Controller", Color.red);
                var o = Resources.Load(fullPath) as T;
                Debug.Log(o);
                return o as T;
            }
            catch (System.Exception)
            {
                BDebug.Log($"Are you sure '{name}' has this component?", "Resource Controller", Color.red);
                throw;
            }
        }
        public T GetObjectComponent<T>(string name, string path = "Default") where T:Object{
            var p = GetRef(path);
            if(p == null)return null;
            try
            {
                var fullPath = p.directory + "/" +name;
                BDebug.Log($"Trying to load '{fullPath}'", "Resource Controller", Color.red);
                var o = Resources.Load(fullPath) as T;
                Debug.Log(o);
                return o as T;
            }
            catch (System.Exception)
            {
                BDebug.Log($"Are you sure '{name}' has this component?", "Resource Controller", Color.red);
                throw;
            }
        }
    }
    [System.Serializable]
    public class ResourceReference{
        public string referenceName = "Default";
        public string directory = "";

        public ResourceReference(string referenceName, string directory)
        {
            this.referenceName = referenceName;
            this.directory = directory;
        }
    }
}
