using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class Rotator : MonoBehaviour
    {
        public Vector3 RotationSpeed;
        private void Start()
        {
        }

        void Update()
        {
            transform.Rotate(360f*RotationSpeed*Time.deltaTime);
        }
    }
}
