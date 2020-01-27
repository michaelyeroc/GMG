using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowKs_Work {
    public class KGameManager : MonoBehaviour
    {
        [SerializeField] //Game Ball
        KBall m_GameBall;
        [SerializeField] //Game Paddle
        KPaddle m_GamePaddle;
        [SerializeField] //Bricks
        List<KBrick> m_Bricks;
        [SerializeField] //Power Ups
        List<KPowerUp> m_PowerUps;

        public static KGameManager GameManager { get; private set; }

        [SerializeField] // Destroy Bricks
        public bool m_DestroyBricks;

        [SerializeField] //Game Pieces
        GameObject m_GamePieces;

        // Start is called before the first frame update
        void Start()
        {
            if (GameManager == null)
            {
                GameManager = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OutOfBounds()
        {
            m_GameBall.ResetBall();
            m_GamePaddle.ResetPaddle();
        }
        
        //Instantiate the power up prefab
        public void SpawnPowerUp(Vector3 pos)
        {
            Instantiate(m_PowerUps[Random.Range(0, m_PowerUps.Count)], pos, Quaternion.identity, m_GamePieces.transform);
        }
    }
}