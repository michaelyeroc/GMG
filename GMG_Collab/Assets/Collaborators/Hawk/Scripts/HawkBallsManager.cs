using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hawk
{
    public class HawkBallsManager : MonoBehaviour
    {
        #region Singleton
        public static HawkBallsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        public List<HawkBall> balls { get; set; }

        [SerializeField]
        private HawkBall ballPrefab;

        private HawkBall initialBall;
        private Rigidbody2D initialBallRB;

        public float initialBallSpeed;

        private HawkGameManger gameManager { get; set; }
        private HawkPaddle paddle { get; set; }

        private void Start()
        {
            gameManager = HawkGameManger.Instance;
            paddle = HawkPaddle.Instance;
            InitBall();
        }

        private void Update()
        {
            if (!gameManager.isgameStarted)
            {
                // stick ball to paddle if game is not started
                Vector3 paddlePosition = paddle.gameObject.transform.position;
                Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);

                initialBall.transform.position = ballPosition;
            }

            if (Input.GetKey(KeyCode.Space) && !gameManager.isgameStarted)
            {
                initialBall.LaunchBall();
                // The game is afoot
                gameManager.isgameStarted = true;
            }
        }

        private void InitBall()
        {
            Vector3 paddlePosition = paddle.gameObject.transform.position;

            Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);

            initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
            initialBallRB = initialBall.GetComponent<Rigidbody2D>();

            balls = new List<HawkBall>
            {
                initialBall
            };
        }

        internal void resetBalls()
        {
            // Destory all then init start ball
            foreach(var ball in balls)
            {
                Destroy(ball.gameObject);
            }

            InitBall();
        }
    }
}