using UnityEngine;
using System;

namespace Hawk
{
    public class HawkBrick : MonoBehaviour
    {
        private SpriteRenderer sr;

        public int hitPoints = 1;

        public static event Action<HawkBrick> OnBrickDestruction;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = HawkBrickManager.Instance.sprites[hitPoints - 1]; // will be deleted later and set in Init.
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HawkBall ball = collision.gameObject.GetComponent<HawkBall>();
            ApplyBallCollision(ball);
        }

        private void ApplyBallCollision(HawkBall ball)
        {
            hitPoints--;

            if (hitPoints <= 0)
            {
                OnBrickDestruction?.Invoke(this);
                // SpawnDestroyEffect();
                Destroy(gameObject);
            }
            else
            {
                // TODO(shf): Change the sprite when implemented levels
            }
        }
    }
}
