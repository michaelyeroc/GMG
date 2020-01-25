using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Hawk
{
    public class HawkCollectableManager : MonoBehaviour
    {
        #region Singleton
        public static HawkCollectableManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        public List<HawkCollectable> availableBuffs;
        public List<HawkCollectable> availableDeBuffs;

        // TODO(shf): List of all live buffs and debuffs and
        // deleting them #onDeath or when game ends or level is beat.

        [Range(0, 100)]
        public float buffChance;
        [Range(0, 100)]
        public float debuffChance;

        private List<HawkCollectable> spawnedCollectables { get; set; }

        private void Start()
        {
            HawkCollectable.removeFromCollectableList += removeFromCollectableList;

            spawnedCollectables = new List<HawkCollectable>();
        }

        public void makeBuff(HawkBrick brick)
        {
            HawkCollectable prefab = availableBuffs[Random.Range(0, availableBuffs.Count)];

            addToCollectableList(Instantiate(prefab, brick.transform.position, Quaternion.identity) as HawkCollectable);
        }

        public void makeDebuff(HawkBrick brick)
        {
            HawkCollectable prefab = availableDeBuffs[Random.Range(0, availableDeBuffs.Count)];

            addToCollectableList(Instantiate(prefab, brick.transform.position, Quaternion.identity) as HawkCollectable);
        }

        public void resetCollectables()
        {
            foreach (HawkCollectable collectable in spawnedCollectables.ToList())
            {
                spawnedCollectables.Remove(collectable);
                Destroy(collectable.gameObject);
            }
        }

        void addToCollectableList(HawkCollectable collectable)
        {
            spawnedCollectables.Add(collectable);
        }

        void removeFromCollectableList(HawkCollectable collectable)
        {
            spawnedCollectables.Remove(collectable);
        }

        private void OnDisable()
        {
            HawkCollectable.removeFromCollectableList -= removeFromCollectableList;
        }
    }
}
