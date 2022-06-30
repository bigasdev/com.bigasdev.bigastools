using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.InputSystem;

namespace BigasTools.UI{
public class ModalHandler : MonoBehaviour
{
    public System.Action onEscPress = delegate{};
    public System.Action onInitialize = delegate{};
    public ModalDirection modalDirection;
    public List<ModalObject> modals;
    public int selectedButton = 0;
    [SerializeField] bool worksOnStart = true;
    float timer = 0;
    private void Start() {
        if(worksOnStart){
            Initialize();
        }
    }
    public void Initialize(){
        var m = GetComponentsInChildren<ModalObject>();
        modals = new List<ModalObject>(m);
        var i = 0;
        foreach(var mo in modals){
            mo.masterHandler = this;
            mo.handlerIndex = i;
            i++;
        }
        SelectWithKeyboard(0);
        onInitialize();
    }
    private void Update() {
        timer += Time.deltaTime;
        if(timer >= .15f){
            OnTick();
        }
        if(BGameInput.Instance.GetKeyPress("Pause")){
            onEscPress();
        }
        if(BGameInput.Instance.GetKeyPress("Interaction")){
            InteractAny(selectedButton);
        }
    }
    void OnTick(){
        if(modals.Count <= 0)return;
        if(modalDirection == ModalDirection.Vertical){
            if(BGameInput.Instance.GetAxis().y == 1){
                SelectWithKeyboard(-1);
                timer = 0;
            }
            if(BGameInput.Instance.GetAxis().y == -1){
                SelectWithKeyboard(1);
                timer = 0;
            }
        }else{
            if(BGameInput.Instance.GetAxis().x == 1){
                SelectWithKeyboard(1);
                timer = 0;
            }
            if(BGameInput.Instance.GetAxis().x == -1){
                SelectWithKeyboard(-1);
                timer = 0;
            }
        }
    }
    public void SelectWithKeyboard(int index){
        DeselectAll();
        selectedButton = selectedButton + index;
        if(selectedButton < 0)selectedButton = modals.Count - 1;
        if(selectedButton > modals.Count - 1)selectedButton = 0;
        modals[selectedButton].OnSelect();
    }
    public void Reset(){
        DeselectAll();
        selectedButton = 0;
    }
    public void SelectAny(int index){
        DeselectAll();
        modals[index]?.OnSelect();
    }
    public void InteractAny(int index){
        modals[index].OnInteract();
    }
    public void DeselectAll(){
        foreach(var m in modals){
            m.OnDeselect();
        }
    }
}
public enum ModalDirection{
    Vertical,
    Horizontal
}

}
