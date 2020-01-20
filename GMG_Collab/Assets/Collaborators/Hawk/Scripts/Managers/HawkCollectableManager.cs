using UnityEngine;

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

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
