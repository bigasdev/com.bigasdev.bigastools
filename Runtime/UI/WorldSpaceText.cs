using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BigasTools.UI{
    public class WorldSpaceText : MonoBehaviour
    {
        public System.Action onType = delegate{};
        public System.Action onStart = delegate{};
        public System.Action onFinish = delegate{};
        [SerializeField] Text textField;
        [SerializeField] AudioClip typingSound;
        [SerializeField] TextRenderSettings renderSettings;
        [SerializeField] TextSpeedSettings speedSettings;
        [SerializeField] TextData textData = new TextData("Your forgot to add the text data.", true);

        public void Render(Vector2 position, TextData textData = null, TextRenderSettings textRenderSettings = null, TextSpeedSettings textSpeedSettings = null){
            if(textRenderSettings!=null)renderSettings = textRenderSettings;
            if(textSpeedSettings!=null)speedSettings = textSpeedSettings;
            if(textData!=null)this.textData = textData;

            this.transform.position = position;

            textField.transform.localScale = new Vector2(1f*renderSettings.GetPPU, 1f*renderSettings.GetPPU);
            textField.color = renderSettings.textColor;
            StartCoroutine(Type(this.textData.text));
        }
        IEnumerator Type(string textInput){
            string temp = "";
            textField.text = temp;
            int idx = 0;
            while(temp.Length < textInput.Length){
                temp += textInput[idx];
                idx++;
                textField.text = temp;
                AudioController.Instance.PlaySound(typingSound);
                if(onType!=null)onType();
                yield return new WaitForSeconds(speedSettings.typeSpeed);
            }
            yield return new WaitForSeconds(speedSettings.endDelay);
            if(textData!=null){
                if(textData.vanish){
                    var tempOpacity = 1f;
                    while(textField.color.a >= 0){
                        tempOpacity -= speedSettings.vanishSpeed * Time.deltaTime;
                        textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, tempOpacity);
                        yield return null;
                    }
                }
            }
            if(onFinish!=null)onFinish();
        }
    }
    [System.Serializable]
    public class TextRenderSettings{
        public Color textColor = Color.white;
        public int pixelsPerUnit;

        public TextRenderSettings(Color textColor, int pixelsPerUnit = 16)
        {
            this.textColor = textColor;
            this.pixelsPerUnit = pixelsPerUnit;
        }

        public float GetPPU{
            get{
                return 1f/pixelsPerUnit;
            }
        }
    }
    [System.Serializable]
    public class TextSpeedSettings{
        public float typeSpeed, endDelay, vanishSpeed;

        public TextSpeedSettings(float typeSpeed, float endDelay, float vanishSpeed)
        {
            this.typeSpeed = typeSpeed;
            this.endDelay = endDelay;
            this.vanishSpeed = vanishSpeed;
        }
    }
    [System.Serializable]
    public class TextData{
        public string text;
        public bool vanish = true;

        public TextData(string text, bool vanish = true)
        {
            this.text = text;
            this.vanish = vanish;
        }
    }
}
