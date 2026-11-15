using Core;
using FMODUnity;
using UnityEngine;

namespace Audio {
    public class BgmTrigger : MonoBehaviour {
        [SerializeField] EventReference eventReference;
        [SerializeField] bool leadMuted;

        void Start() {
            var service = ServiceManager.Instance.GetService<FMODService>();
            service.PlayBGM(eventReference, leadMuted: leadMuted);
        }
    }
}