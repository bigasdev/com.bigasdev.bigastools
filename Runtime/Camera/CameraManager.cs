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
        [HideInInspector] public UnityEvent<float> OnZoom = new UnityEvent<float>();
#endregion

#region camera-variables
        [SerializeField] bool debug;
        
        [Header("Camera settings")]
        [SerializeField] Updates workOn = Updates.LATE;
        [SerializeField] Transform cameraHolder;
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
#region zoom-variables
        [Header("Zoom settings")]
        [SerializeField] protected float zoomSpeed = 4f;
        protected float originalCameraSize;
        protected float zoomFactor;
#endregion
        /// <summary>
        /// This region will be focused on the basic camera functions as well as the workflow of it.
        /// </summary>
        private void Awake() {
            OnAwake();
        }
        protected virtual void OnAwake(){
            if(debug)BDebug.Log($"Starting camera at position: {this.transform.position}", "Camera", Color.green);
        }
        private void Update() {
            if(workOn == Updates.UPDATE){
                Work();
            }
        }
        private void FixedUpdate() {
            if(workOn == Updates.FIXED){
                Work();
            }
        }
        private void LateUpdate() {
            if(workOn == Updates.LATE){
                Work();
            }
        }
        protected virtual void Work(){
            FollowTarget();
            HandleZoom();
        }
        protected virtual void FollowTarget(){
            if(Target == null)return;
            float xTarget = Target.position.x + offset.x;
            float yTarget = Target.position.y + offset.y;
            //float zTarget = Target.position.z + offset.z;

            float xNew = this.transform.position.x;
            if(!hasXLocked){
                xNew = Mathf.Lerp(this.transform.position.x, xTarget, Time.deltaTime*followSpeed);
            }
            float yNew = this.transform.position.y;
            if(!hasYLocked){
                yNew = Mathf.Lerp(this.transform.position.y, yTarget, Time.deltaTime*followSpeed);
            }
            this.transform.position = new Vector3(xNew, yNew, transform.position.z);
        }
        protected virtual void HandleZoom(){
            float targetSize = originalCameraSize - zoomFactor;
            if(targetSize != currentCamera.orthographicSize){
                currentCamera.orthographicSize = Mathf.Lerp(currentCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            }
        }
#region publicfunctions
        //Region for every function that will be called from other scripts.
        public virtual void SetTarget(Transform target, bool xLocked = false, bool yLocked = false, string reason = ""){
            if(debug)BDebug.Log($"Camera is now following: {target}, {reason}", "Camera", Color.green);
            Target = target;
            hasXLocked = xLocked;
            hasYLocked = yLocked;
            OnTargetChange.Invoke(target);
        }
        public virtual void SetZoom(float zoomAmount, float zoomSpeed = 4f, string reason=""){
            if(debug)BDebug.Log($"Camera is now zooming: {zoomAmount}, {reason}", "Camera", Color.green);
            zoomFactor = zoomAmount;
            this.zoomSpeed = zoomSpeed;
            OnZoom.Invoke(zoomAmount);
        }
#endregion
    }
    public enum Updates{
        UPDATE,
        FIXED,
        LATE
    }
}
