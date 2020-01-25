using UnityEngine;

namespace Hawk
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HawkPaddle : MonoBehaviour
    {
        #region Singleton Paddle
        private static HawkPaddle instance;

        public static HawkPaddle Instance => instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        #endregion

        private Rigidbody2D paddle;
        // The paddle speed of the script in unity controls
        // the actual paddle speed. This seems to have no effect
        // so maybe I'm doing something wrong
        [SerializeField]
        private readonly float m_PaddleSpeed = 8f;

        private HawkBallsManager ballManager { get; set; }

        // Start is called before the first frame update
        private void Start()
        {
            paddle = GetComponent<Rigidbody2D>();
            ballManager = HawkBallsManager.Instance;
        }

        // Update is called once per frame
        private void Update()
        {
            PaddleMovement();
        }

        private void PaddleMovement()
        {
            Vector2 newVelocity = Vector2.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
                newVelocity.x += -m_PaddleSpeed;
            if (Input.GetKey(KeyCode.RightArrow))
                newVelocity.x += m_PaddleSpeed;

            paddle.velocity = newVelocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("HawkBall"))
            {
                Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

                Vector3 hitPoint = collision.contacts[0].point;

                Vector3 paddleCenter = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

                ballRb.velocity = Vector2.zero;

                float difference = paddleCenter.x - hitPoint.x;

                float speed = ballManager.initialBallSpeed;
                if (hitPoint.x < paddleCenter.x)
                {
                    // left side of paddle
                    ballRb.velocity = new Vector2(-Mathf.Abs(difference * speed), speed);
                }
                else
                {
                    ballRb.velocity = new Vector2(Mathf.Abs(difference * speed), speed);
                }
            }
        }
    }
}
