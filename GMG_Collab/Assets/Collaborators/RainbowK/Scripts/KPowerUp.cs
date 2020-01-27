using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RainbowKs_Work {
    public class KPowerUp : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "BottomScreenEdge")
            {
                Destroy(gameObject);
                Debug.Log("Missed Power Up");
            }
        }
    }
}