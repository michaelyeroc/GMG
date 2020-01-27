using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowKs_Work {
    public class KBrick : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        //called when brick is destroyed
        public void DropPowerUp()
        {
            KGameManager.GameManager.SpawnPowerUp(transform.position);
        }
    }
}