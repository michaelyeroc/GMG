using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class FakeGravity : MonoBehaviour
    {
        public GameObject Parent;
        private Rigidbody2D _rigidBody;
        private float _gravityMagnitude;
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _rigidBody.gravityScale = 0f;

            _gravityMagnitude = Physics2D.gravity.magnitude;
        }

        void Update()
        {
            var direction = Parent.transform.position - transform.position;
            var force = direction.normalized * _gravityMagnitude;

            _rigidBody.AddForceAtPosition(force, _rigidBody.centerOfMass);
        }
    }
}
