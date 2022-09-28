using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BigasTools{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager instance;
        public static CameraManager Instance{
            get{
                if(instance == null)instance = FindObjectOfType<CameraManager>();
                return instance;
            }
        }
#region code-functions
        [HideInInspector] public UnityEvent<Transform> OnTargetChange = new UnityEvent<Transform>();
#endregion

#region camera-variables
        [SerializeField] bool debug;
        
        [Header("Camera settings")]
        [SerializeField] Camera currentCamera;
        [SerializeField] float followSpeed = 4f;
        [SerializeField] Vector3 offset;
        public bool hasXLocked = false;
        public bool hasYLocked = false;
        protected Transform target;

        public Transform Target{
            get{
                return target;
            }set{
                target = value;
            }
        }
#endregion
        private void Awake() {
            if(debug)BDebug.Log($"Starting camera at position: {this.transform.position}", "Camera", Color.green);
        }
#region publicfunctions
        public void SetTarget(Transform target, bool xLocked = false, bool yLocked = false, string reason = ""){
            if(debug)BDebug.Log($"Camera is now following: ${target}, {reason}", "Camera", Color.green);
            Target = target;
            hasXLocked = xLocked;
            hasYLocked = yLocked;
        }
#endregion
    }
}
