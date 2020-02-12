using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class Block : MonoBehaviour
    {
        private void Start()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
