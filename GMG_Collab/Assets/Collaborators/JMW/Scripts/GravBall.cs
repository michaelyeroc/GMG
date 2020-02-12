using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class GravBall : MonoBehaviour
    {
        private void Start()
        {

        }
        
        void Update()
        {
            // TODO: Probably do some cool stuff with near misses here, also move delete logic to blackhole
            /*
            var blackHoleLocation = Vector2.zero;

            if(Vector2.Distance(blackHoleLocation, transform.position) < 1)
            {
                Time.timeScale = 0.25f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            */
        }
    }
}
