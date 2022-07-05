using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigasTools{
    public class CooldownController : MonoBehaviour
    {
        private static CooldownController instance;
        public static CooldownController Instance{
            get{
                if(instance == null) instance = FindObjectOfType<CooldownController>();
                return instance;
            }
        }
        public List<CdLoopData> loopDatas = new List<CdLoopData>();

        float timer = 0;

        private void Update() {
            timer += Time.deltaTime;
            if(timer >= .01f){
                try
                {
                    foreach(var l in loopDatas){
                        l.Update(timer);
                    }
                }
                catch (System.Exception)
                {
                    
                }
                timer = 0;
            }
        }

        public void AddLoop(float time, System.Action action, Entity entity = null){
            var ld = new CdLoopData(time, action);

            if(entity!=null){
                entity.onDispose += ()=>{
                    loopDatas.Remove(ld);
                };
            }

            loopDatas.Add(ld);
        }
        public void CheckCurrentLoops(){
            foreach(var ld in loopDatas){
                BDebug.Log(ld, "CooldownController");
            }
        }
        public void DisposeAllLoops(){
            loopDatas.Clear();
        }
    }
    [System.Serializable]
    public class CdLoopData{
        public float time;
        public System.Action action;
        float timer;

        public CdLoopData(float time, Action action)
        {
            this.time = time;
            this.action = action;
        }
        public void Update(float tick){
            timer += tick;
            if(timer >= time){
                action();
                timer = 0;
            }
        }
    }
}
