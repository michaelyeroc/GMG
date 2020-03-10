using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coreys_Work
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        [SerializeField]
        Ball m_Ball = null;
        Rigidbody2D m_PaddleRigidbody;
        [SerializeField]
        float m_PaddleSpeed = 200f;

        Vector3 m_StartPos;

        // Start is called before the first frame update
        void Start()
        {
            m_PaddleRigidbody = GetComponent<Rigidbody2D>();
            m_StartPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Ball != null && m_Ball.BallMoving == false && Input.GetKeyDown(KeyCode.Space))
                m_Ball.LaunchBall(0, 1);

            if (m_Ball != null && m_Ball.BallMoving)
            {
                Vector2 newVelocity = Vector2.zero;

                if (GameManager.TheGameManager.CurrentGameType == GameType.BREAKOUT)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                        newVelocity.x += -m_PaddleSpeed;
                    if (Input.GetKey(KeyCode.RightArrow))
                        newVelocity.x += m_PaddleSpeed;
                }
                else if (GameManager.TheGameManager.CurrentGameType == GameType.PONG)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                        newVelocity.y += m_PaddleSpeed;
                    if (Input.GetKey(KeyCode.DownArrow))
                        newVelocity.y += -m_PaddleSpeed;
                }

                m_PaddleRigidbody.velocity = newVelocity;
            }
        }

        public void ResetPaddle()
        {
            transform.position = m_StartPos;
            if (m_PaddleRigidbody != null)
                m_PaddleRigidbody.velocity = Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "PowerUp(Clone)")
            {
                Debug.Log("Power Up Picked Up");
                Destroy(collision.gameObject);
            }
        }
    }
}