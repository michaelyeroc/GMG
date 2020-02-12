using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vince_Stuff
{

    public class Ball : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float ballForce;
        bool gameStarted = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space) && gameStarted == false)
            {
                transform.SetParent(null);
                rb.isKinematic = false;
                rb.AddForce(new Vector2(ballForce, ballForce));
                gameStarted = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (ContactPoint2D contact in col.contacts)
            {
                var colName = contact.collider.name;
                switch (colName)
                {
                    case "PaddleLeft":
                        rb.velocity = new Vector2(-ballForce, ballForce);
                        break;
                    case "PaddleCenter":
                        rb.velocity = new Vector2(0, ballForce);
                        break;
                    case "PaddleRight":
                        rb.velocity = new Vector2(ballForce, ballForce);
                        break;

                }
            }

        }
    }
}
