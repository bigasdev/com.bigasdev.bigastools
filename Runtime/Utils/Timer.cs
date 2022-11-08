using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BigasTools{
    public class Timer {
        public float elapsed;
        public float setTime;
        public bool looping;
        public bool paused = false;

        public event Action OnComplete = delegate { };
        public event Action OnPause = delegate { };

        public bool complete {
            get { return elapsed >= setTime; }
        }
        
        public float reverseElapsed{
            get {return setTime - elapsed; }
        }

        public Timer(float _set, bool looping = false, Action _complete = null) {
            setTime = _set;
            this.looping = looping;
            if(_complete != null)OnComplete += _complete;
        }

        public void Update() {
            if(paused || complete)return;
            elapsed += Time.deltaTime;

            if (elapsed >= setTime) {
                if(looping)Reset();
                OnComplete();
            }
            
        }

        public void Pause(){
            OnPause();
            paused = !paused;
        }

        public void Reset() {
            elapsed = 0;
        }
    }
}
