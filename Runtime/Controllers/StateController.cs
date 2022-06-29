using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigasTools{
    public class StateController : MonoBehaviour
    {
        private static StateController instance;
        public static StateController Instance{
            get{
                if(instance == null){
                    instance = FindObjectOfType<StateController>();
                }
                return instance;
            }
        }
        public States currentState;
        States lastState;
        
        //Change the current state and save the last one.
        public void ChangeState(States state){
            if(state == currentState){
                return;
            }
            lastState = currentState;
            currentState = state;
        }
        //Return to the update state;
        public void ReturnToInitialState(){
            currentState = States.GAME_UPDATE;
        }
        //Return to the last registered state and save the new one
        public void ReturnToLastState(){
            ChangeState(lastState);
        }

    }
    public enum States{
        GAME_UPDATE,
        GAME_MENU,
        GAME_DEATH,
        GAME_LISTENING,
        GAME_INTERACTING,
        GAME_IDLE
    }
}
