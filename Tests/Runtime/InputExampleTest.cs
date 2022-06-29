using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BigasTools.InputSystem;

namespace BigasTools.Test{
    public class InputExampleTest : MonoBehaviour
    {
        [SerializeField] Text canvasText;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(BGameInput.Instance.GetKeyPress("Interaction")){
                canvasText.text = "YOU PRESSED INTERACTION";
            }
            if(BGameInput.Instance.GetKeyPress("Pause")){
                canvasText.text = "YOU PRESSED PAUSE";
            }

            this.transform.position += new Vector3(BGameInput.Instance.GetAxis().x, BGameInput.Instance.GetAxis().y) * Time.deltaTime;
        }
    }
}
