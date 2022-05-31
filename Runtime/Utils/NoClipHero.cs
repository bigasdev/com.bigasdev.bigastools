using System.Collections;
using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// * This is the core class for the no clip hero, a tool to help with debugging
public class NoClipHero : MonoBehaviour
{
    // * The core functions, you can use this to have even more help.
    public System.Action onInteract;
    public System.Action onMove;
    public System.Action<Collider2D> onTouchEntity;
    public System.Action onQuit;

    // * The stats for the hero
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask collisionsMask;
    [SerializeField] float interactionRadius;
    [SerializeField] KeyCode actionKey;

    // ? You can see the cordinates of the hero with a UI if you want so
    [SerializeField] Text cordsText;
    
    // * The local components of the hero
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite normalSprite, collidedSprite;

    // ? Local variables

    Vector2 movement;
    Vector2 Pos(){
        return new Vector2((int)this.transform.position.x, (int)this.transform.position.y);
    }
    

    // * We'll use this function to build the hero in any script we want.
    public NoClipHero ConstructHero(Vector2 startingPos, System.Action onInteract = null, System.Action onMove = null, System.Action onQuit = null, System.Action<Collider2D> onTouchEntity = null){
        this.transform.position = startingPos;
        if(onInteract != null)this.onInteract += onInteract;
        if(onMove != null)this.onMove += onMove;
        if(onTouchEntity != null)this.onTouchEntity += onTouchEntity;
        if(onQuit != null)this.onQuit += onQuit;
        return this;
    }
    private void Update() {
        GetMovement();
        HandleInteractions();

        if(cordsText != null)cordsText.text = $"Cords: {Pos().x.ToString()}, {Pos().y.ToString()}";

        if(Input.GetKeyDown(actionKey)){
            if(onInteract != null)onInteract();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Quit();
        }
    }

    private void FixedUpdate() {
        rb.velocity = movement * moveSpeed * Time.deltaTime;
    }
    
    void GetMovement(){
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        
        movement = new Vector2(x , y).normalized;
        if(x == 1 || x == -1 || y == 1 || y == 1){
            if(onMove != null)onMove();
        }
    }

    void HandleInteractions(){
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, interactionRadius, transform.up, 1f, collisionsMask);
        if(hit.collider != null){
            if(collidedSprite != null)this.spriteRenderer.sprite = collidedSprite;
            if(onTouchEntity != null)onTouchEntity(hit.collider);
        }else{
            if(normalSprite != null)this.spriteRenderer.sprite = normalSprite;
        }
    }
    void Quit(){
        if(onQuit != null)onQuit();
        Destroy(this.gameObject);
    }
}
