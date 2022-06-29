using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace BigasTools.InputSystem{
    public class BGameInput : MonoBehaviour
    {
        private static BGameInput instance;
        public static BGameInput Instance{
            get{
                if(instance == null)instance = FindObjectOfType<BGameInput>();
                return instance;
            }
        }
        public InputData inputData;
        public Gamepad currentGamePad{
            get{
                return Gamepad.current;
            }
        }
        public bool GetKeyPress(string name){
            var i = inputData.profile.Where(x=>x.inputName == name).FirstOrDefault();
            if(i == null){
                BDebug.Log($"You haven't assigned the profile for key {name}", "GameInput", Color.blue, true);
                return false;
            }
            if(Input.GetKeyDown(i.inputKey) || GetJoysticKeyPress(i.joystickKey)){
                BDebug.Log($"Key Pressed: {name}", "GameInput", Color.blue, true);
                return true;
            }
            return false;
        }
        public bool GetKey(string name){
            var i = inputData.profile.Where(x=>x.inputName == name).FirstOrDefault();
            if(i == null){
                BDebug.Log($"You haven't assigned the profile for key {name}", "GameInput", Color.blue, true);
                return false;
            }
            if(Input.GetKey(i.inputKey) || GetJoysticKey(i.joystickKey)){
                BDebug.Log($"Key Hold: {name}", "GameInput", Color.blue, true);
                return true;
            }
            return false;
        }
        public Vector2 GetAxis(){
            Vector2 axis = Vector2.zero;
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
                axis =  new Vector2(1, axis.y);
            }
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
                axis =  new Vector2(-1, axis.y);
            }
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
                axis =  new Vector2(axis.x, 1);
            }
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
                axis =  new Vector2(axis.x, -1);
            }
            if(currentGamePad != null){
                var jax = currentGamePad.leftStick.ReadUnprocessedValue();
                var check = currentGamePad.leftStick.ReadValue();
                if(check != Vector2.zero){
                    return jax;
                }
            }
            return axis;
        }
        bool GetJoysticKeyPress(InputKeys key){
            if(currentGamePad == null)return false;
            switch(key){
                case InputKeys.A:
                    return currentGamePad.aButton.wasPressedThisFrame;
                case InputKeys.B:
                    return currentGamePad.bButton.wasPressedThisFrame;
                case InputKeys.X:
                    return currentGamePad.xButton.wasPressedThisFrame;
                case InputKeys.Y:
                    return currentGamePad.yButton.wasPressedThisFrame;
                case InputKeys.PAUSE:
                    return currentGamePad.startButton.wasPressedThisFrame;
                case InputKeys.SELECT:
                    return currentGamePad.aButton.wasPressedThisFrame;
                case InputKeys.LEFT_STICK:
                    return currentGamePad.bButton.wasPressedThisFrame;
                case InputKeys.RIGHT_STICK:
                    return currentGamePad.xButton.wasPressedThisFrame;
                case InputKeys.LEFT_SHOULDER:
                    return currentGamePad.yButton.wasPressedThisFrame;
                case InputKeys.LEFT_TRIGGER:
                    return currentGamePad.startButton.wasPressedThisFrame;
                case InputKeys.RIGHT_SHOULDER:
                    return currentGamePad.yButton.wasPressedThisFrame;
                case InputKeys.RIGHT_TRIGGER:
                    return currentGamePad.startButton.wasPressedThisFrame;
            }
            return false;
        }
        bool GetJoysticKey(InputKeys key){
            if(currentGamePad == null)return false;
            switch(key){
                case InputKeys.A:
                    return currentGamePad.aButton.isPressed;
                case InputKeys.B:
                    return currentGamePad.bButton.isPressed;
                case InputKeys.X:
                    return currentGamePad.xButton.isPressed;
                case InputKeys.Y:
                    return currentGamePad.yButton.isPressed;
                case InputKeys.PAUSE:
                    return currentGamePad.startButton.isPressed;
                case InputKeys.SELECT:
                    return currentGamePad.aButton.isPressed;
                case InputKeys.LEFT_STICK:
                    return currentGamePad.bButton.isPressed;
                case InputKeys.RIGHT_STICK:
                    return currentGamePad.xButton.isPressed;
                case InputKeys.LEFT_SHOULDER:
                    return currentGamePad.yButton.isPressed;
                case InputKeys.LEFT_TRIGGER:
                    return currentGamePad.startButton.isPressed;
                case InputKeys.RIGHT_SHOULDER:
                    return currentGamePad.yButton.isPressed;
                case InputKeys.RIGHT_TRIGGER:
                    return currentGamePad.startButton.isPressed;
                    
            }
            return false;
        }
    }
    [System.Serializable]
    public class InputData{
        public List<InputProfile> profile;
        public InputData(){
            var first = new InputProfile("Interaction", KeyCode.E, InputKeys.A);
            var second = new InputProfile("Pause", KeyCode.Escape, InputKeys.PAUSE);
            var third = new InputProfile("Info", KeyCode.CapsLock, InputKeys.SELECT);

            profile = new List<InputProfile>();
            profile.Add(first);
            profile.Add(second);
            profile.Add(third);
        }
    }
    [System.Serializable]
    public class InputProfile{
        public string inputName;
        public KeyCode inputKey;
        public InputKeys joystickKey;

        public InputProfile(string inputName, KeyCode inputKey, InputKeys joystickKey)
        {
            this.inputName = inputName;
            this.inputKey = inputKey;
            this.joystickKey = joystickKey;
        }
    }
    public enum InputKeys{
        A,
        B,
        X,
        Y,
        PAUSE,
        SELECT,
        LEFT_STICK,
        RIGHT_STICK,
        RIGHT_TRIGGER,
        RIGHT_SHOULDER,
        LEFT_TRIGGER,
        LEFT_SHOULDER

    }
}
