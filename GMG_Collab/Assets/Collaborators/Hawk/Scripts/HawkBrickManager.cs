using UnityEngine;
using System.Collections.Generic;
using System;

namespace Hawk
{
    public class HawkBrickManager : MonoBehaviour
    {
        #region Singleton Brick Manager
        private static HawkBrickManager instance;

        public static HawkBrickManager Instance => instance;

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

        public Sprite[] sprites;

        public List<int[,]> levels { get; set; }

        public List<HawkBrick> remainingBricks { get; set; }
        public HawkBrick brickPrefab;
        public Color[] brickColors;
        private GameObject bricksContainer;
        // Initial position for first brick
        private float initialBrickSpawnX = -3f;
        // In Unity it sees it as 3.8 Y axis which is what we want
        // but here it needs to be one higher...
        private float initialBrickSpawnY = 4.8f;
        // TODO(shf): The brick width plus a minor gap Make dynamic
        private float shiftAmount = 1f;

        private int initialBrickCount { get; set; }

        private int maxRows = 17;
        private int maxCols = 12;
        // level selector we can change in unity
        // This should really be controlled by the
        // GameMangeger script.
        // TODO(shf): Figure out how to move to game manager
        public int currentLevel;

        private void Start()
        {
            bricksContainer = new GameObject("bricksContainer");
            remainingBricks = new List<HawkBrick>();
            levels = loadLevels();
            makeBricks();
        }

        private void makeBricks()
        {
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
    }
}
