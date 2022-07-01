using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.InputSystem;
using BigasTools;
using BigasTools.UI;
public class WorldSpaceUIExample : MonoBehaviour
{
    [SerializeField] WorldSpaceText worldSpaceObj;
    private void Update() {
        if(BGameInput.Instance.GetKeyPress("Interaction")){
            var view = Instantiate(worldSpaceObj);
            view.onFinish += ()=>{
                Destroy(view.gameObject);
            };
            view.Render(new Vector2(0,0), new TextData("First example! Just the simple one", true));
        }
        if(BGameInput.Instance.GetKeyPress("Example")){
            var view = Instantiate(worldSpaceObj);
            view.onFinish += ()=>{
                Destroy(view.gameObject);
            };
            view.Render(new Vector2(2,3), new TextData("Second example! With faster typing", true), new TextRenderSettings(Color.green, 32), new TextSpeedSettings(.03f, .5f, 2f));
        }
        if(BGameInput.Instance.GetKeyPress("Example1")){
            var view = Instantiate(worldSpaceObj);
            view.onFinish += ()=>{
                Destroy(view.gameObject);
            };
            view.Render(new Vector2(-2,-3), new TextData("Third example! With everything slow", true), new TextRenderSettings(Color.red, 32), new TextSpeedSettings(.1f, 1.5f, .3f));
        }
        if(BGameInput.Instance.GetKeyPress("Example2")){
            var view = Instantiate(worldSpaceObj);
            view.onFinish += ()=>{
                Destroy(view.gameObject);
            };
            view.Render(new Vector2(-4,-1), new TextData("Last example!! Its bigger and delayed!", true), new TextRenderSettings(Color.magenta, 16), new TextSpeedSettings(.05f, 2.5f, .3f));
        }
    }
}
