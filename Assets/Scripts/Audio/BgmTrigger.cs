using Core;
using FMODUnity;
using UnityEngine;

namespace Audio {
    public class BgmTrigger : MonoBehaviour {
        
        [SerializeField] EventReference eventReference;
        [SerializeField] bool leadMuted;
        
        void Start() {
            MusicService service = ServiceManager.Instance.GetService<MusicService>();
            service.Play(eventReference, leadMuted: leadMuted);
        }
    }
}
