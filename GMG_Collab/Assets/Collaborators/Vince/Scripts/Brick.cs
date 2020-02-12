using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Vince_Stuff
{
    public class Brick : MonoBehaviour
    {
        public Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(col.gameObject.tag == "Ball")
            {
                Destroy(gameObject);
            }
        }
    }
}
