using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coreys_Work
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        float m_BallSpeed = 200f;
        Rigidbody2D m_BallRigidBody;

        public bool BallMoving { get; private set; } = false;

        private void Start()
        {
            m_BallRigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LaunchBall()
        {
            BallMoving = true;
            m_BallRigidBody.velocity = new Vector2(0, m_BallSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                var colName = contact.collider.name;
                switch (colName)
                {
                    case "PaddleLeft":
                        m_BallRigidBody.velocity = new Vector2(-m_BallSpeed, m_BallSpeed);
                        break;
                    case "PaddleCenter":
                        m_BallRigidBody.velocity = new Vector2(0, m_BallSpeed);
                        break;
                    case "PaddleRight":
                        m_BallRigidBody.velocity = new Vector2(m_BallSpeed, m_BallSpeed);
                        break;
                    case "Brick":
                        Destroy(contact.collider.gameObject);
                        break;
                    case "Bottom Bounds":
                        Debug.Log("Hit Bottom");
                        break;
                }
            }
        }
    }
}
