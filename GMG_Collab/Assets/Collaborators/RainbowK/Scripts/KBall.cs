using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowKs_Work
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class KBall : MonoBehaviour
    {
        [SerializeField] //Ball Speed
        float m_BallSpeed = 200f;
        [SerializeField] //Ball Rigid Body
        Rigidbody2D m_BallRigidBody;
        [SerializeField] //Ball Moving
        public bool BallMoving { get; set; } = false;
        [SerializeField] //Start Pos
        Vector3 m_StartPosition;
        // Start is called before the first frame update
        void Start()
        {
            //get attached rigidbody on this component
            m_BallRigidBody = GetComponent<Rigidbody2D>();
            //use starting position for where the ball should come back to
            m_StartPosition = transform.position;
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
            transform.position = m_StartPosition;
            BallMoving = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("KBrick"))
            {
                Debug.Log("Destroy the brick and create a power up");
                if (KGameManager.GameManager.m_DestroyBricks)
                {
                    collision.gameObject.GetComponent<KBrick>().DropPowerUp();
                    Destroy(collision.gameObject);
                }
                else
                {
                    collision.gameObject.GetComponent<KBrick>().DropPowerUp();
                    collision.gameObject.SetActive(false);
                }
            }

            foreach (ContactPoint2D contact in collision.contacts)
            {
                var colName = contact.collider.name;
                Debug.Log($"Hit collider {colName}");
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
                    case "BottomScreenEdge":
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "BottomScreenEdge")
            {
                Debug.Log("Hit Bottom Trigger");
                KGameManager.GameManager.OutOfBounds();
            }
        }
    }
}