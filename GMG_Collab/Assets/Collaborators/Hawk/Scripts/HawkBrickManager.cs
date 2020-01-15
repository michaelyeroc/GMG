using UnityEngine;

namespace Hawk
{
    public class HawkBrickManager : MonoBehaviour
    {
        #region Singleton Brick Manager
        private static HawkBrickManager instance;

        public static HawkBrickManager Instance => instance;

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

        public Sprite[] sprites;
    }
}
