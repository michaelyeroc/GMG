using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/*
 * This game was made following Damage Software's tutorial
 * on youtube. Besides a little bit of refactoring and small
 * changes the majority of this code is straight from the tutorial.
 * 
 */
namespace Hawk
{
    public class HawkGameManger : MonoBehaviour
    {
        #region Singleton
        public static HawkGameManger Instance { get; private set; }

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
                levels = loadLevels();
            }
        }
        #endregion

        public GameObject gameOverScreen;
        public GameObject victoryScreen;

        public static event Action<int> onLifeLost;

        public int availableLives = 3;
        public int lives { get; set; }
        public bool isgameStarted { get; set; }
        public List<int[,]> levels { get; set; }
        public readonly int maxRows = 17;
        public readonly int maxCols = 12;
        // level selector we can change in unity
        public int currentLevel;

        // Game Managers
        private HawkBallsManager ballManager;
        private HawkBrickManager brickManager;

        private void Start()
        {
            lives = availableLives;
            ballManager = HawkBallsManager.Instance;
            brickManager = HawkBrickManager.Instance;

            Screen.SetResolution(540, 900, false);
            HawkBall.onDeath += onDeath;
            HawkBrick.onBrickDestruction += brickDestruction;
        }

        private List<int[,]> loadLevels()
        {
            TextAsset text = Resources.Load("levels") as TextAsset;

            string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<int[,]> levelsTemp = new List<int[,]>();
            int[,] currentLevel = new int[maxRows, maxCols];

            int currentRow = 0;

            for (int row = 0; row < rows.Length; row++)
            {
                string line = rows[row];

                if (line.IndexOf("--") == -1)
                {
                    // parsing row
                    string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int col = 0; col < bricks.Length; col++)
                    {
                        currentLevel[currentRow, col] = int.Parse(bricks[col]);
                    }

                    currentRow++;
                }
                else
                {
                    // end of current level
                    // add the matrix to the list and continue the loop
                    currentRow = 0;
                    levelsTemp.Add(currentLevel);
                    currentLevel = new int[maxRows, maxCols];
                }
            }

            return levelsTemp;
        }

        void onDeath(HawkBall ball)
        {
            if (ballManager.balls.Count <= 0)
            {
                lives--;
                if (lives < 1)
                {
                    gameOverScreen.SetActive(true);
                }
                else
                {
                    onLifeLost?.Invoke(lives);
                    // reset balls
                    // stop game
                    // reload level
                    ballManager.resetBalls();
                    isgameStarted = false;
                    brickManager.reloadBricks();
                }
            }
        }

        void brickDestruction(HawkBrick brick)
        {
            if (brickManager.remainingBricks.Count <= 0)
            {
                // cleared level
                ballManager.resetBalls();
                isgameStarted = false;
                loadNextLevel();
            }
        }

        void loadNextLevel()
        {
            currentLevel++;
            if (currentLevel >= levels.Count)
            {
                victoryScreen.SetActive(true);
            }
            else
            {
                brickManager.setLevel(currentLevel);
                brickManager.reloadBricks();
            }
        }

        private void OnDisable()
        {
            // unsubscribe from event
            HawkBall.onDeath -= onDeath;
            HawkBrick.onBrickDestruction -= brickDestruction;
        }

        // Restart wired up to death screen button
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
