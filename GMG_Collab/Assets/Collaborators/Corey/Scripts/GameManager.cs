using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coreys_Work
{
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

        public int Score { get; set; } = 0;
        public int NumLives { get; set; } = 3;
        public static GameManager TheGameManager { get; private set; }


        public bool m_DestroyBricks;
        [SerializeField]
        bool m_FullRestart;

        [SerializeField]
        GameObject m_GamePieces;
        // Start is called before the first frame update
        void Start()
        {
            if (TheGameManager == null)
            {
                TheGameManager = this;
            }
        }

        // Update is called once per frame
        void Update()
        {

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
            Instantiate(m_PowerUps[Random.Range(0, m_PowerUps.Count)], pos, Quaternion.identity, m_GamePieces.transform);
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
            SceneManager.LoadScene("MainMenu");
        }
    }
}