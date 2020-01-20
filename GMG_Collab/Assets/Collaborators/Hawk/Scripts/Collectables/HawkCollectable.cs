using UnityEngine;

namespace Hawk
{
    public abstract class HawkCollectable : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "paddle")
            {
                Destroy(gameObject);
                applyEffect();
            }

            if (collision.tag == "deathwall")
            {
                Destroy(gameObject);
            }
        }

        protected abstract void applyEffect();
    }
}
