using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coreys_Work
{
    public enum GameType
    {
        BREAKOUT,
        PONG
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        Ball m_GameBall;
        [SerializeField]
        Paddle m_GamePaddle;
        [SerializeField]
        List<Brick> m_Bricks;
        [SerializeField]
        List<PowerUp> m_PowerUps;
        [SerializeField]
        GameType m_CurrentGameType = GameType.BREAKOUT;
        public GameType CurrentGameType { get => m_CurrentGameType; set => m_CurrentGameType = value; }

        public int Score { get; set; } = 0;
        public int NumLives { get; set; } = 3;
        public static GameManager TheGameManager { get; private set; }


        public bool m_DestroyBricks;
        [SerializeField]
        bool m_FullRestart;

        [SerializeField]
        List<GameCanvas> m_CanvasList;
        GameCanvas m_CurrGameCanvas;

        //[SerializeField]
        //Text m_LivesText;

        private void Awake()
        {
            if (TheGameManager != null && TheGameManager != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                TheGameManager = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            m_CurrGameCanvas = SelectGameCanvasByGameType(TheGameManager.CurrentGameType);
            if (m_CurrGameCanvas != null)
            {
                switch (TheGameManager.CurrentGameType)
                {
                    case GameType.BREAKOUT:
                        SelectBricksForPowerUp();
                        break;
                    case GameType.PONG:
                        break;
                    default:
                        break;
                }
                m_CurrGameCanvas.Canvas.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Game Canvas is null.");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        GameCanvas SelectGameCanvasByGameType(GameType gameType)
        {
            if (m_CanvasList != null && m_CanvasList.Count > 0)
            {
                return m_CanvasList.Find(x => x.GameType == gameType);
            }
            return null;
        }

        public void RestartGame()
        {
            NumLives = 3;
            Score = 0;
            m_GameBall.ResetBall();
            m_GamePaddle.ResetPaddle();
            Time.timeScale = 1;

            if (m_FullRestart)
            {
                foreach (Brick brick in m_Bricks)
                {
                    brick.gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = (int)(m_Bricks.Count * 0.5f); i < m_Bricks.Count; i++)
                {
                    m_Bricks[i].gameObject.SetActive(true);
                }
            }
        }

        public void OutOfBounds()
        {
            NumLives--;
            if (NumLives > 0)
            {
                m_GameBall.ResetBall();
                m_GamePaddle.ResetPaddle();
            }
            else
            {
                m_GameBall.ResetBall();
                m_GamePaddle.ResetPaddle();
                m_GameBall.enabled = false;
            }
        }

        public void SpawnPowerUp(Vector3 pos)
        {
            if (m_CurrGameCanvas != null)
                Instantiate(m_PowerUps[Random.Range(0, m_PowerUps.Count)], pos, Quaternion.identity, m_CurrGameCanvas.GamePieces.transform);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnPauseGame()
        {
            Time.timeScale = 1;
        }

        public void QuitGame()
        {
            SceneManager.LoadScene("Main Menu");
        }

        void SelectBricksForPowerUp()
        {
            foreach (Brick brick in m_Bricks)
            {
                if (Random.Range(0, 10) >= 5)
                    brick.CanDrop = true;
            }
        }
    }
}