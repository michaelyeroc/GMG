namespace Hawk
{
    public class HawkMultiball : HawkCollectable
    {
        private HawkBallsManager ballManager;

        private void Start()
        {
            ballManager = HawkBallsManager.Instance;
        }

        protected override void applyEffect()
        {
            foreach(HawkBall ball in ballManager.balls)
            {
                ballManager.spawnBalls(ball.gameObject.transform.position, 2);
            }
        }
    }
}
