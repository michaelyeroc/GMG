using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class PaddleController : MonoBehaviour
    {
        public float Speed = Mathf.PI;
        private CircularVelocity circularVelocity;
        private void Start()
        {
            circularVelocity = GetComponent<CircularVelocity>();
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.A)) {
                circularVelocity.Velocity.x = -Speed;
            } else if(Input.GetKey(KeyCode.D)) {
                circularVelocity.Velocity.x = Speed;
            } else {
                circularVelocity.Velocity.x = 0;
            }
        }
    }
}
