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

        public bool BallMoving { get; set; } = false;

        Vector3 m_StartPos;

        private void Start()
        {
            m_BallRigidBody = GetComponent<Rigidbody2D>();
            m_StartPos = transform.position;
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

        public void ResetBall()
        {
            m_BallRigidBody.velocity = Vector2.zero;
            transform.position = m_StartPos;
            BallMoving = false;
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
                        if (GameManager.TheGameManager.m_DestroyBricks)
                        {
                            contact.collider.GetComponent<Brick>().DropPowerUp();
                            Destroy(contact.collider.gameObject);
                        }
                        else
                        {
                            contact.collider.GetComponent<Brick>().DropPowerUp();
                            contact.collider.gameObject.SetActive(false);
                        }
                        break;
                    case "Bottom Bounds":
                        Debug.Log("Hit Bottom");
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Bottom Bounds")
            {
                Debug.Log("Hit Bottom Trigger");
                GameManager.TheGameManager.OutOfBounds();
            }
        }
    }
}
