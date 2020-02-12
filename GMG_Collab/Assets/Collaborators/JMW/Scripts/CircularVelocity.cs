using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class CircularVelocity : MonoBehaviour
    {
        public Vector2 Velocity;
        private CircularTransform circularTransform;
        private void Start()
        {
            circularTransform = GetComponent<CircularTransform>();
        }

        void Update()
        {
            circularTransform.Position += Velocity*Time.deltaTime;
        }
    }
}
