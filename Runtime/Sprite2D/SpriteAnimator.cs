using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BigasTools.Sprite2D{
    public class SpriteAnimator : MonoBehaviour
    {
        public SpriteAnimatorData idleAnimation;
        /// <summary>
        /// The current animation, normally it would be the idle one but for cases of looping stuff we'll set new animations here
        /// </summary>
        SpriteAnimatorData currentAnimation;
        [SerializeField] SpriteRenderer spriteRenderer;
        Timer animationTimer;
        private void Start() {
            currentAnimation = idleAnimation;
            animationTimer = new Timer(currentAnimation.tickTimer, true);
            animationTimer.OnComplete += Animation;
        }
        private void FixedUpdate() {
            animationTimer.Update();
        }
        /// <summary>
        /// Our main function, here we'll create the handler for idle and looping animations
        /// </summary>
        void Animation(){
            currentAnimation.Tick(spriteRenderer);
        }
    }
    [System.Serializable]
    public class SpriteAnimatorData{
        public System.Action onFinishCycle = delegate{};
        public System.Action onStop = delegate{};
        public UnityEvent onFinishCycleEvent;
        public UnityEvent onStopEvent;
        public string objectName;
        public GameObject gameObject;
        public Sprite[] sprites;
        public float tickTimer;
        public bool loop = false;
        public SpriteAnimatorData nextAnimation;
        public int currentAnimation;
        public int animationLenght{
            get{return sprites.Length;}
        }
        public void NextAnim(){
            currentAnimation++;
            if(currentAnimation >= animationLenght){
                if(!loop){
                    if(onStop != null)onStop();
                    if(onStopEvent != null)onStopEvent.Invoke();
                }
                if(onFinishCycle != null)onFinishCycle();
                if(onFinishCycleEvent != null)onFinishCycleEvent.Invoke();
                currentAnimation = 0;
            }
        }
        public void Tick(SpriteRenderer spriteRenderer){
            spriteRenderer.sprite = sprites[currentAnimation];
            NextAnim();
        }
    }
}
