using UnityEngine;

namespace Hawk
{
    public class HawkDeath : MonoBehaviour
    {
        HawkBallsManager ballManager;

        // Start is called before the first frame update
        void Start()
        {
            ballManager = HawkBallsManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("HawkBall"))
            {
                HawkBall ball = collision.GetComponent<HawkBall>();
                ballManager.balls.Remove(ball);
                ball.Die();
            }
        }
    }
}