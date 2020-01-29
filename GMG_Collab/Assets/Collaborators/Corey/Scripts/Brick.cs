using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coreys_Work
{
    public class Brick : MonoBehaviour
    {
        bool m_CanDrop = false;

        public bool CanDrop
        {
            get
            {
                return m_CanDrop;
            }

            set
            {
                m_CanDrop = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DropPowerUp()
        {
            if (m_CanDrop)
                GameManager.TheGameManager.SpawnPowerUp(transform.position);
        }
    }
}
