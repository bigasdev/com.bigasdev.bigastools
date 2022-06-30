using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace BigasTools.UI{
public class ModalObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent onClickAction;
    public UnityEvent onSelectAction, onDeselectAction;
    [SerializeField] Orientation2D pointerDirection;
    [SerializeField] Sprite pointer;
    GameObject pointerObj;
    public Image baseImage;
    [HideInInspector] public ModalHandler masterHandler;
    public int handlerIndex = 0;
    private void Awake() {
        
    }
    public void OnSelect(){
        onSelectAction?.Invoke();
        NativeSelect();
    }
    public void OnInteract(){
        onClickAction?.Invoke();
    }
    public void OnDeselect(){
        onDeselectAction?.Invoke();
        pointerObj?.SetActive(false);
    }
    void NativeSelect(){
        if(pointerObj == null){
            pointerObj = new GameObject();
            pointerObj.transform.parent = this.transform;
            pointerObj.AddComponent<Image>().sprite = pointer;
            pointerObj.GetComponent<Image>().SetNativeSize();
            SetPointerPos(pointerDirection, pointerObj.GetComponent<RectTransform>());
            pointerObj.transform.rotation = Quaternion.Euler(0,0,(int)pointerDirection);
        }else{
            pointerObj.SetActive(true);
        }
    }
    void SetPointerPos(Orientation2D orientation2D, RectTransform pointer){
        pointer.pivot = new Vector2(0, .5f);
        switch(orientation2D){
            case Orientation2D.Down:
                pointer.anchorMin = new Vector2(0.5f, 1);
                pointer.anchorMax = new Vector2(0.5f, 1);
                pointer.anchoredPosition = new Vector2(0,-25);
            break;
            case Orientation2D.Up:
                pointer.anchorMin = new Vector2(0.5f, 0f);
                pointer.anchorMax = new Vector2(0.5f, 0f);
                pointer.anchoredPosition = new Vector2(0,25);
            break;
            case Orientation2D.Left:
                pointer.anchorMin = new Vector2(1f, .5f);
                pointer.anchorMax = new Vector2(1f, .5f);
                pointer.anchoredPosition = new Vector2(-25,0);
            break;
            case Orientation2D.Right:
                pointer.anchorMin = new Vector2(0f, .5f);
                pointer.anchorMax = new Vector2(0f, .5f);
                pointer.anchoredPosition = new Vector2(25,0);
            break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        masterHandler.DeselectAll();
        masterHandler.SelectAny(handlerIndex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        masterHandler.DeselectAll();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnInteract();
    }
}
public enum Orientation2D{
    Left = 0,
    Up = 270,
    Right = 180,
    Down = 90
}

}
