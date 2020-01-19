using System;
using UnityEngine;

namespace Hawk
{
    public class HawkBall : MonoBehaviour
    {
        public static event Action<HawkBall> OnDeath;

        [SerializeField]
        private Rigidbody2D ballRigidBody2D;

        private HawkGameManger gameManager { get; set; }
        private HawkBallsManager ballManager { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            ballRigidBody2D = GetComponent<Rigidbody2D>();
            gameManager = HawkGameManger.Instance;
            ballManager = HawkBallsManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void LaunchBall()
        {
            ballRigidBody2D.AddForce(new Vector2(0, ballManager.initialBallSpeed));
        }

        internal void Die()
        {
            Destroy(gameObject, 0.3f);
            OnDeath?.Invoke(this);
        }
    }
}