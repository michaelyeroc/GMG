using System;
using UnityEngine;

namespace Hawk
{
    public abstract class HawkCollectable : MonoBehaviour
    {
        public static event Action<HawkCollectable> removeFromCollectableList;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "paddle")
            {
                removeFromCollectableList?.Invoke(this);

                Destroy(gameObject);
                applyEffect();
            }

            if (collision.tag == "deathwall")
            {
                removeFromCollectableList?.Invoke(this);

                Destroy(gameObject);
            }
        }

        protected abstract void applyEffect();
    }
}
