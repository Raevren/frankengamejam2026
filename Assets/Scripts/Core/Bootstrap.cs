using Audio;
using UnityEngine;

namespace Core {
    public class Bootstrap : MonoBehaviour {
        static Bootstrap instance;
        ServiceManager _serviceManager;

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
            _serviceManager.RegisterService<BgmPlayer>();
            _serviceManager.RegisterService<PlayerService>();
            _serviceManager.RegisterService<SongService>();
            // All services should be registered by now
            _serviceManager.OnAllRegistered();
        }
    }
}
