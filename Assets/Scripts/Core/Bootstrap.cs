using Audio;
using UnityEngine;

namespace Core {
    public class Bootstrap : MonoBehaviour {
        static Bootstrap instance;
        ServiceManager _serviceManager;
        
        [SerializeField]
        SongService songService;
        [SerializeField] 
        AudioConfig audioConfig;

        void Awake() {
            if (instance != null && instance != this) {
                Destroy(gameObject);
                return;
            }

            instance = this;
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