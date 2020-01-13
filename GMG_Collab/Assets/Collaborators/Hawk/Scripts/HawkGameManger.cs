using UnityEngine;

namespace Hawk
{
    public class HawkGameManger : MonoBehaviour
    {
        #region Singleton Game Manager
        private static HawkGameManger instance;

        public static HawkGameManger Instance => instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        #endregion

        public bool isgameStarted { get; set; }
    }
}
