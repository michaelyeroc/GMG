using UnityEngine;
using System;
using static UnityEngine.ParticleSystem;

namespace Hawk
{
    public class HawkBrick : MonoBehaviour
    {
        private SpriteRenderer sr;

        public int hitPoints = 1;
        public ParticleSystem destoryEffect;

        public static event Action<HawkBrick> onBrickDestruction;

        private HawkBrickManager brickManager { get; set; }

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            brickManager = HawkBrickManager.Instance;
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
                brickManager.remainingBricks.Remove(this);

                onBrickDestruction?.Invoke(this);
                DestroyEffect();
                Destroy(gameObject);
            }
            else
            {
                sr.sprite = brickManager.sprites[hitPoints - 1];
            }
        }

        private void DestroyEffect()
        {
            Vector3 brickPosition = gameObject.transform.position;
            Vector3 effectPosition = new Vector3(brickPosition.x, brickPosition.y, brickPosition.z - 0.2f);
            GameObject effect = Instantiate(destoryEffect.gameObject, effectPosition, Quaternion.identity);

            MainModule mm = effect.GetComponent<ParticleSystem>().main;
            mm.startColor = sr.color;
            Destroy(effect, destoryEffect.main.startLifetime.constant);
        }

        internal void Init(Transform container, Sprite sprite, Color color, int hp)
        {
            transform.SetParent(container);
            sr.sprite = sprite;
            sr.color = color;
            hitPoints = hp;
        }
    }
}
