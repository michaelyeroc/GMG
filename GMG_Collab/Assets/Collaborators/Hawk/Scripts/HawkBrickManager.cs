using UnityEngine;
using System.Collections.Generic;
using System;

namespace Hawk
{
    public class HawkBrickManager : MonoBehaviour
    {
        #region Singleton
        public static HawkBrickManager Instance { get; private set; }

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

        public Sprite[] sprites;

        public List<HawkBrick> remainingBricks { get; set; }
        public HawkBrick brickPrefab;
        public Color[] brickColors;
        private GameObject bricksContainer;
        // Initial position for first brick
        private float initialBrickSpawnX = -3.5f;
        // In Unity it sees it as 3.8 Y axis which is what we want
        // but here it needs to be one higher...
        private float initialBrickSpawnY = 4.8f;
        // TODO(shf): The brick width plus a minor gap Make dynamic
        private float shiftAmount = 1f;

        private int initialBrickCount { get; set; }

        // Levels and column information from
        // the game manager
        private List<int[,]> levels;
        private int maxRows = 17;
        private int maxCols = 12;
        public int currentLevel;

        private void Start()
        {
            // Get a reference to the levels
            HawkGameManger manger = HawkGameManger.Instance;
            levels = manger.levels;
            maxRows = manger.maxRows;
            maxCols = manger.maxCols;
            currentLevel = manger.currentLevel;

            bricksContainer = new GameObject("bricksContainer");
            makeBricks();
        }

        private void makeBricks()
        {
            remainingBricks = new List<HawkBrick>();
            int[,] level = levels[currentLevel];

            float currentSpawnX = initialBrickSpawnX;
            float currentSpawnY = initialBrickSpawnY;
            // used to make each new brick closer to camera to avoid
            //  overlapping
            float zShift = 0;

            for (int row = 0; row < maxRows; row++)
            {
                for (int col = 0; col < maxCols; col++)
                {
                    int brickType = level[row, col];

                    if (brickType > 0)
                    {
                        HawkBrick brick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as HawkBrick;
                        brick.Init(bricksContainer.transform, sprites[brickType - 1], brickColors[brickType], brickType);

                        remainingBricks.Add(brick);
                        zShift += 0.0001f;
                    }

                    currentSpawnX += shiftAmount;

                    if (col + 1 == maxCols)
                    {
                        currentSpawnX = initialBrickSpawnX;
                    }
                }

                currentSpawnY -= shiftAmount;
            }
            initialBrickCount = remainingBricks.Count;
        }

        internal void reloadLevel()
        {
            clearRemainingBricks();
            makeBricks();
        }

        private void clearRemainingBricks()
        {
            foreach (HawkBrick brick in remainingBricks)
            {
                Destroy(brick.gameObject);
            }
        }
    }
}
