using Audio;
using UnityEngine;

namespace Core {
    public class Bootstrap : MonoBehaviour {
        public static Bootstrap Instance { get; private set; }
        [field: SerializeField] public AudioConfig AudioConfig { get; private set; }
        ServiceManager _serviceManager;
        
        [SerializeField]
        SongService songService;

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _serviceManager = new ServiceManager();
            RegisterServices();
        }

        void RegisterServices() {
            //Register all services
            _serviceManager.RegisterService<MusicService>();
            _serviceManager.RegisterService<PlayerService>();
            _serviceManager.RegisterService(songService);
            // All services should be registered by now
            _serviceManager.OnAllRegistered();
        }
    }
}