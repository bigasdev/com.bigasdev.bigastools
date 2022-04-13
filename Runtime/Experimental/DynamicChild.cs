using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigasTools{
    public class DynamicChild : MonoBehaviour
    {
        //
        // We will set the pool name to return to it here.
        //
        public string dynamicPoolName;

        //
        // If it's a particle you can set the stop action as callback and change this
        //
        private void OnParticleSystemStopped() {
            DynamicPool.Instance.ReturnToPool(dynamicPoolName, this.gameObject);
        }
    }
}
