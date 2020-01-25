using UnityEngine;
using UnityEngine.UI;

namespace Hawk
{
    public class HawkUIManager : MonoBehaviour
    {
        #region Singleton
        public static HawkUIManager Instance { get; private set; }

        // Good to know Awake runs before Start
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
            // get instance of managers
            gameManager = HawkGameManger.Instance;
            brickManager = HawkBrickManager.Instance;

            // Event subscriptions
            HawkBrick.onBrickDestruction += onBrickDestruciton;
            HawkBrickManager.onBricksLoaded += onBricksLoaded;
            HawkGameManger.onLifeLost += onLifeLost;
        }
        #endregion

        public Text target;
        public Text scoreText;
        public Text livesText;

        private HawkGameManger gameManager;
        private HawkBrickManager brickManager;

        private int score { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            onLifeLost(gameManager.availableLives);
            // For when we restart level
            // TODO(shf): We can probably do better
            // once we look into non-static events
            updateRemainingBricks();
        }

        // subscribed to level loaded event in game manager
        void onBricksLoaded()
        {
            updateRemainingBricks();
            updateScoreText(0);
        }

        void onLifeLost(int lives)
        {
            livesText.text = $"LIVES: {lives}";
        }

        void onBrickDestruciton(HawkBrick obj)
        {
            updateRemainingBricks();
            updateScoreText(10);
        }

        void updateRemainingBricks()
        {
            target.text = $"TARGET:\n{brickManager.remainingBricks.Count}/{brickManager.initialBrickCount}";
        }

        void updateScoreText(int increment)
        {
            score += increment;

            scoreText.text = $"SCORE:\n{score}";
        }

        private void OnDisable()
        {
            HawkBrick.onBrickDestruction -= onBrickDestruciton;
            HawkBrickManager.onBricksLoaded -= onBricksLoaded;
            HawkGameManger.onLifeLost -= onLifeLost;
        }
    }
}
