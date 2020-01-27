using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowKs_Work {
    public class KPaddle : MonoBehaviour
    {
        [SerializeField] //Ball
        KBall m_Ball = null;
        [SerializeField] //Paddle Rigid Body
        Rigidbody2D m_PaddleRigidBody;
        [SerializeField] //Paddle Speed
        float m_PaddleSpeed = 200f;
        [SerializeField] // Start Position
        Vector3 m_StartPosition;
        // Start is called before the first frame update
        void Start()
        {
            //get attached rigidbody on this component
            m_PaddleRigidBody = GetComponent<Rigidbody2D>();
            //use starting position for where the paddle should come back to
            m_StartPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            //when space bar is pressed, launch the ball
            if (m_Ball != null && m_Ball.BallMoving == false && Input.GetKeyDown(KeyCode.Space))
                m_Ball.LaunchBall();

            if (m_Ball != null && m_Ball.BallMoving)
            {
                var newVelocity = Vector2.zero;

                if (Input.GetKey(KeyCode.LeftArrow))
                    newVelocity.x += -m_PaddleSpeed;
                if (Input.GetKey(KeyCode.RightArrow))
                    newVelocity.x += m_PaddleSpeed;

                m_PaddleRigidBody.velocity = newVelocity;
            }
        }

        public void ResetPaddle()
        {
            transform.position = m_StartPosition;
            m_PaddleRigidBody.velocity = Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("KPowerUp"))
            {
                Debug.Log("Power Up Picked Up");
                Destroy(collision.gameObject);
            }            
        }
    }
}