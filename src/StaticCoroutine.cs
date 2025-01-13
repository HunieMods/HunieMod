namespace HunieMod
{
    /// <summary>
    /// A class that allows starting a <see cref="Coroutine"/> without the need to for being inside a <see cref="MonoBehaviour"/>.
    /// </summary>
    public class StaticCoroutine : MonoBehaviour
    {
        private static StaticCoroutine instance;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// Executes <see cref="MonoBehaviour.StartCoroutine(IEnumerator)"/> on the specified <paramref name="routine"/>.
        /// </summary>
        /// <param name="routine">The coroutine to start.</param>
        /// <returns>The instance of the <see cref="Coroutine"/> that was created while starting the routine.</returns>
        public static Coroutine Do(IEnumerator routine)
        {
            if (instance == null)
            {
                GameObject obj = new("Static Coroutine Object")
                {
                    hideFlags = HideFlags.HideInHierarchy
                };
                instance = obj.AddComponent<StaticCoroutine>();
                DontDestroyOnLoad(obj);
            }
            return instance.StartCoroutine(routine);
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}
