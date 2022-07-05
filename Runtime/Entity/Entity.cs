using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.InputSystem;
using BigasTools.UI;
using UnityEngine.UI;

namespace BigasTools{
    public class Entity : MonoBehaviour
    {
        //Important stuff from the package
        public AudioController audioController{
            get{return AudioController.Instance;}
        }
        public ResourceController resourceController{
            get{return ResourceController.Instance;}
        }
        public StateController stateController{
            get{return StateController.Instance;}
        }
        public CooldownController cooldown{
            get{return CooldownController.Instance;}
        }
        public BGameInput input{
            get{return BGameInput.Instance;}
        }

        //*Important variables about rendering and whatnot
        SpriteRenderer spriteRendererCache;
        public SpriteRenderer spriteRenderer{
            get{
                if(spriteRendererCache == null)spriteRendererCache = GetComponentInChildren<SpriteRenderer>();
                return spriteRendererCache;
            }
        }

        //Set or check if the spriteRenderer is visible or not
        public bool isVisible{
            get{
                if(spriteRenderer == null)return false;
                return spriteRenderer.enabled;
            }set{
                if(spriteRenderer == null)return;
                spriteRenderer.enabled = value;
            }
        }
        //The X and Y size for the current sprite texture
        public float pixelsX{
            get{
                try
                {
                    return spriteRenderer.sprite.textureRect.width;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            }
        }
        public float pixelsY{
            get{
                try
                {
                    return spriteRenderer.sprite.textureRect.height;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            }
        }
        //The squash values
        public float squashX{
            get{
                return this.transform.localScale.x;
            }set{
                var v = new Vector2(value, 2-value);
                this.transform.localScale = v;
                squashValue = v;
            }
        }
        public float squashY{
            get{
                return this.transform.localScale.y;
            }set{
                var v = new Vector2(2-value, value);
                this.transform.localScale = v;
                squashValue = v;
            }
        }
        //The horizontal direction, important to make the entity turn
        public int dir{
            get{
                if(spriteRenderer == null)return 0;
                return (int)spriteRenderer.transform.localScale.x;
            }set{
                if(spriteRenderer == null)return;
                spriteRenderer.transform.localScale = new Vector3(value, spriteRenderer.transform.localScale.y);
            }
        }
        //Important to blink effects and what not
        public Material currentMaterial{
            get{
                if(spriteRenderer == null)return null;
                return spriteRenderer.material;
            }set{
                if(spriteRenderer == null)return;
                spriteRenderer.material = value;
            }
        }

        //*Important game variable
        public float moveSpeedVariable = 4;
        public float lifeVariable, maxLifeVariable;
        public float life{
            get{
                return lifeVariable;
            }set{
                lifeVariable = value;
                if(lifeVariable <= 0){
                    Kill();
                }
            }
        }
        public float maxLife{
            get{
                return maxLifeVariable;
            }set{
                maxLifeVariable = value;
            }
        }
        public float moveSpeed{
            get{
                return moveSpeedVariable;
            }set{
                moveSpeedVariable = value;
            }
        }
        public float spriteSquashSpeed = 0.125f;
        public Material blinkMaterial;


        //*Important variables about positioning
        //The current position of the entity
        public Vector2 currentPosition{
            get{
                return this.transform.position;
            }
            set{
                this.transform.position = value;
            }
        }
        //Coordinates to help with positioning and raycasting
        public Vector2 right{
            get{
                return new Vector2(this.transform.position.x + 1, this.transform.position.y);
            }
        }
        public Vector2 left{
            get{
                return new Vector2(this.transform.position.x - 1, this.transform.position.y);
            }
        }
        public Vector2 up{
            get{
                return new Vector2(this.transform.position.x, this.transform.position.y + 1);
            }
        }
        public Vector2 down{
            get{
                return new Vector2(this.transform.position.x, this.transform.position.y - 1);
            }
        }

        //* Displosable stuff
        public System.Action onDispose = delegate{};


        //*Local variables
        [HideInInspector] protected Vector2 startingPos = Vector2.zero;
        [HideInInspector] protected Vector2 moveToPosition = Vector2.zero;
        [HideInInspector] protected Vector2 focusPosition = Vector2.zero;
        [HideInInspector] protected Vector2 startingScale = Vector2.zero;
        [HideInInspector] protected Vector2 squashValue = Vector2.zero;

        [HideInInspector] protected Entity lastAttacker = null;
        [HideInInspector] protected Material originalMaterial = null;
        [HideInInspector] protected float startingMoveSpeed = 0;
        [HideInInspector] protected float shakeX, shakeY, currentShakeTimer = 0, shakeTimer;
        [HideInInspector] protected float blinkingFor, blinkingTimer;
        [HideInInspector] protected int blinkLoops;
        [HideInInspector] protected Color blinkColor;
        [HideInInspector] protected WorldSpaceText currentWorldSpaceText = null;

        //! You can use this function to add checks for you start
        protected virtual void OnSpawn(){
            if(audioController == null)BDebug.LogWarning("Is missing the audiocontroller", $"gameobject: {this.gameObject.name}");
            if(resourceController == null)BDebug.LogWarning("Is missing the resourcecontroller", $"gameobject: {this.gameObject.name}");
            if(stateController == null)BDebug.LogWarning("Is missing the statecontroller", $"gameobject: {this.gameObject.name}");
            if(input == null)BDebug.LogWarning("Is missing the input system", $"gameobject: {this.gameObject.name}");
            if(spriteRenderer == null)BDebug.LogWarning("Is missing the sprite renderer", $"gameobject: {this.gameObject.name}");

            /*
            BDebug.Log(pixelsX, $"{this.gameObject.name}");
            BDebug.Log(pixelsY, $"{this.gameObject.name}");
            BDebug.Log(dir, $"{this.gameObject.name}");
            BDebug.Log(currentPosition, $"{this.gameObject.name}");
            BDebug.Log(isVisible, $"{this.gameObject.name}");
            BDebug.Log(currentMaterial, $"{this.gameObject.name}");

            cooldown.AddLoop(.5f, ()=>{
                squashY = .725f;
            }, this);*/

            startingMoveSpeed = moveSpeed;
            startingPos = this.transform.position;
            originalMaterial = spriteRenderer.material;
            startingScale = spriteRenderer.transform.localScale;

        }

        private void Start() {
            OnSpawn();
        }
        private void Update() {
            DirFocus();
            OnSquash();
            OnBlink();
            OnShake();
            OnMove();
        }
        //? Vanilla movement update function
        protected virtual void OnMove(){
            if(moveToPosition != Vector2.zero){
                currentPosition = Vector2.MoveTowards(currentPosition, moveToPosition, moveSpeed * Time.deltaTime);
                if(currentPosition == moveToPosition){
                    moveToPosition = Vector2.zero;
                    focusPosition = Vector2.zero;
                }
            }
        }

        //? Function to move the character, it works in the fixedupdate
        public virtual void MoveTo(Vector2 position){
            moveToPosition = position;
            focusPosition = position;
        }
        //? Update function to ensure the sprite will always rotate to the focusposition
        protected virtual void DirFocus(){
            if(focusPosition == Vector2.zero){
                dir = 1;
                return;
            }
            var d = this.transform.position.x - focusPosition.x;
            if(focusPosition.x < transform.position.x){
                dir = -1;
            }
            if(focusPosition.x > transform.position.x){
                dir = 1;
            }
        }
        //? Update function to squash the sprite
        protected virtual void OnSquash(){
            if(squashValue == Vector2.zero)return;
            if(new Vector2(this.transform.localScale.x, this.transform.localScale.y) == startingScale){
                squashValue = Vector2.zero;
            }
            this.transform.localScale = Vector2.MoveTowards(this.transform.localScale, startingScale, spriteSquashSpeed * Time.deltaTime);
        }
        //? Action function to trigger a shake with force paramters and a timer
        public virtual void Shake(float xPow, float yPow, float timer = .125f){
            shakeTimer = timer;
            shakeX = xPow;
            shakeY = yPow;
        }
        //? Action and update function for the sprite blink
        public virtual void Blink(float timer, int loops = 1, Color? color = null){
            var c = color == null ? Color.white : color.Value;
            blinkColor = c;
            blinkingFor = timer;
            currentMaterial = blinkMaterial;
            spriteRenderer.color = c;
            blinkLoops = loops;
        }
        protected virtual void OnBlink(){
            if(blinkLoops <= 0)return;

            blinkingTimer += Time.deltaTime;
            if(blinkingTimer>=blinkingFor){
                var m = currentMaterial == originalMaterial ? blinkMaterial : originalMaterial;
                var c = spriteRenderer.color == Color.white ? blinkColor : Color.white;
                currentMaterial = m;
                spriteRenderer.color = c;
                blinkLoops--;
                blinkingTimer = 0;
                if(blinkLoops <= 0){
                    currentMaterial = originalMaterial;
                    spriteRenderer.color = Color.white;
                    blinkingFor = 0;
                }
            }
        }
        //? Update function to shake the spriterenderer 
        protected virtual void OnShake(){
            if(shakeX == 0 && shakeY == 0)return;

            currentShakeTimer += Time.deltaTime;
            if(currentShakeTimer <= shakeTimer){
                spriteRenderer.transform.localPosition = new Vector2(0 + Random.insideUnitCircle.x * shakeX, 0 + Random.insideUnitCircle.y * shakeY);
                return;
            }

            currentShakeTimer = 0;
            shakeX = 0;
            shakeY = 0;
            shakeTimer = 0;
            spriteRenderer.transform.localPosition = Vector2.zero;
        }
        //? Action function to initiate the life of the entity from script
        public virtual void InitLife(float life){
            this.life = life;
            maxLife = life;
        }

        //? Functions to deal with damage and killing an entity
        public virtual void Hit(float damage, Entity attacker = null){
            squashX = .525f;
            Blink(.1f);
            life -= damage;
        }
        public virtual void Kill(){
            if(onDispose!=null)onDispose();
            Dispose();
        }
        protected virtual void Dispose(){
            this.gameObject.SetActive(false);
        }

        //? Functions to deal with entity talking through the worldspacetext
        public virtual void Talk(TextData textData = null, TextRenderSettings textRenderSettings = null, TextSpeedSettings textSpeedSettings = null){
            squashY = .7f;
            if(currentWorldSpaceText == null){
                var c = resourceController.GetObject<GameObject>("WorldSpaceUI", "UI");
                if(c==null)return;
                var obj = Instantiate(c);
                currentWorldSpaceText = obj.GetComponent<WorldSpaceText>();
                currentWorldSpaceText.Render(new Vector2(spriteRenderer.transform.position.x, spriteRenderer.transform.position.y + 1), textData, textRenderSettings, textSpeedSettings);
                currentWorldSpaceText.onFinish += ()=>{currentWorldSpaceText.gameObject.SetActive(false);};
                return;
            }
            currentWorldSpaceText.gameObject.SetActive(true);
            currentWorldSpaceText.Render(new Vector2(spriteRenderer.transform.position.x, spriteRenderer.transform.position.y + 1), textData, textRenderSettings, textSpeedSettings);
        }
    }
}
