using UnityEngine;

namespace Hawk
{
    public class HawkBall : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D ballRigidBody2D;

        // Start is called before the first frame update
        void Start()
        {
            ballRigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void LaunchBall()
        {
            ballRigidBody2D.velocity = new Vector2(0, HawkBallsManager.Instance.initialBallSpeed);
        }
    }
}