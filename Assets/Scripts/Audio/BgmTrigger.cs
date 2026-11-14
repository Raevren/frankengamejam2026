using Core;
using FMODUnity;
using UnityEngine;

namespace Audio {
    public class BgmTrigger : MonoBehaviour {
        
        [SerializeField] private EventReference eventReference;
        
        void Start() {
            ServiceManager.Instance.GetService<BgmPlayer>().Play(eventReference);
        }
    }
}
