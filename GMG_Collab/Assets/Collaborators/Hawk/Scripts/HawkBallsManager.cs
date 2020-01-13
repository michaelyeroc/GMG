using System.Collections.Generic;
using UnityEngine;

namespace Hawk
{
    public class HawkBallsManager : MonoBehaviour
    {
        #region Singleton Ball Manager
        private static HawkBallsManager instance;

        public static HawkBallsManager Instance => instance;

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

        public List<HawkBall> balls { get; set; }

        [SerializeField]
        private HawkBall ballPrefab;
        private HawkBall initialBall;
        private Rigidbody2D initialBallRB;

        public float initialBallSpeed = 15;

        private void Start()
        {
            InitBall();
        }

        private void Update()
        {
            if (!HawkGameManger.Instance.isgameStarted)
            {
                // stick ball to paddle if game is not started
                Vector3 paddlePosition = HawkPaddle.Instance.gameObject.transform.position;
                Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);

                initialBall.transform.position = ballPosition;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                initialBallRB.isKinematic = false;
                initialBallRB.AddForce(new Vector2(0, initialBallSpeed));

                HawkGameManger.Instance.isgameStarted = true;
            }
        }

        private void InitBall()
        {
            Vector3 paddlePosition = HawkPaddle.Instance.gameObject.transform.position;

            Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);

            initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
            initialBallRB = initialBall.GetComponent<Rigidbody2D>();

            balls = new List<HawkBall>
            {
                initialBall
            };
        }
    }
}