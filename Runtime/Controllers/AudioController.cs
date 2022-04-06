using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasMath;

namespace BigasTools{
    public class AudioController : MonoBehaviour
    {
        private static AudioController instance;
        public static AudioController Instance{
            get{
                if(instance == null){
                    instance = FindObjectOfType<AudioController>();
                }
                return instance;
            }
        }
        [SerializeField] AudioSource sfxSource, musicSource, ambientSource;

        /// <summary>
        /// Play a sound using the clip name stored in the folder from your resourcecontroller
        /// </summary>
        /// <param name="name"></param>
        public void PlaySound(string name){
            var audio = ResourceController.GetAudio(name);
            if(audio == null)return;
            sfxSource.PlayOneShot(audio);
        }
        /// <summary>
        /// Stop the current playing sound
        /// </summary>
        public void StopSound(){
            sfxSource.Stop();
        }
    }
}
