using UnityEngine;
using System;
using System.Collections.Generic;

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

        [Range(0, 100)]
        public float buffChance;
        [Range(0, 100)]
        public float debuffChance;
    }
}
