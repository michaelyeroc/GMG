using UnityEngine;
using System;
using System.Collections.Generic;

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

        public bool isgameStarted { get; set; }
        public List<int[,]> levels { get; set; }
        public readonly int maxRows = 17;
        public readonly int maxCols = 12;
        // level selector we can change in unity
        public int currentLevel;

        private void Start()
        {
            Screen.SetResolution(540, 900, false);
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
