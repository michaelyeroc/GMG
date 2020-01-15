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

        // Start is called before the first frame update
        void Start()
        {
            m_PaddleRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Ball != null && m_Ball.BallMoving == false && Input.GetKeyDown(KeyCode.Space))
                m_Ball.LaunchBall();

            if (m_Ball != null && m_Ball.BallMoving)
            {
                Vector2 newVelocity = Vector2.zero;

                if (Input.GetKey(KeyCode.LeftArrow))
                    newVelocity.x += -m_PaddleSpeed;
                if (Input.GetKey(KeyCode.RightArrow))
                    newVelocity.x += m_PaddleSpeed;

                m_PaddleRigidbody.velocity = newVelocity;
            }
        }
    }
}