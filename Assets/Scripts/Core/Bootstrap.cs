using Audio;
using UnityEngine;

namespace Core {
    public class Bootstrap : MonoBehaviour {
        public static Bootstrap Instance { get; private set; }
        ServiceManager _serviceManager;
        
        [SerializeField]
        SongService songService;
        [SerializeField] 
        AudioConfig audioConfig;

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
            _serviceManager.RegisterService(new FMODService(audioConfig));
            _serviceManager.RegisterService<PlayerService>();
            _serviceManager.RegisterService(songService);
            // All services should be registered by now
            _serviceManager.OnAllRegistered();
        }
    }
}