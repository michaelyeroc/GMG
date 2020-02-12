using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class CircularTransform : MonoBehaviour
    {
        public Vector2 Position;
        public Transform Parent;

        public bool AlignTransform;
        private void Start()
        {
            if( Position == Vector2.zero)
            {
                var origin = GetOrigin();
                var angle = Vector2.Angle(origin, transform.position);
                var distance = Vector2.Distance(origin, transform.position);
                
                angle = Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Sign(transform.position.x);
                
                Position = new Vector2(angle, distance);

                UpdatePosition();
            }
        }

        Vector2 GetOrigin()
        {
            return Parent != null ? (Vector2)Parent.transform.parent.position : Vector2.zero;
        }

        void UpdatePosition()
        {
            transform.position = GetOrigin() + new Vector2(Mathf.Sin(Position.x), Mathf.Cos(Position.x)) * Position.y;

            if(AlignTransform)
            {
                transform.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * Position.x);
            }
        }

        void Update()
        {
            UpdatePosition();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
        }
    }
}
