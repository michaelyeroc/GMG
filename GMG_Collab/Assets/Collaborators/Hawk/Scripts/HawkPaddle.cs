using UnityEngine;

namespace Hawk
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HawkPaddle : MonoBehaviour
    {
        private Rigidbody2D paddle;
        // The paddle speed of the script in unity controls
        // the actual paddle speed. This seems to have no effect
        // so maybe I'm doing something wrong
        [SerializeField]
        private float m_PaddleSpeed = 15f;

        // Start is called before the first frame update
        private void Start()
        {
            paddle = GetComponent<Rigidbody2D>();
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
    }
}
