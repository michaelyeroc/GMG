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

        public List<HawkBall> balls;

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
            balls = new List<HawkBall>();
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
            
            balls.Add(initialBall);
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

        internal void spawnBalls(Vector3 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                HawkBall spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as HawkBall;

                Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
                spawnedBallRb.AddForce(new Vector2(0, initialBallSpeed));

                // This is causing Invalid operation exceptions while the game is running
                // producing some funny results. Also it's only ever trying to spawn more balls
                // off the initial ball which if it has died cause a null pointer and no new balls.
                // Which is interesting because it's supposed to go through each ball but the invalid
                // operation exception much be causing some issues too
                balls.Add(spawnedBall);
            }
        }
    }
}