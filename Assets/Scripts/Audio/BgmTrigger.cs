using Core;
using FMODUnity;
using Level;
using UnityEngine;

namespace Audio {
    public class BgmTrigger : MonoBehaviour {
        [SerializeField] SongAsset songAsset;
        [SerializeField] bool leadMuted;

        void Start() {
            var service = ServiceManager.Instance.GetService<FMODService>();
            service.PlayBGM(songAsset.audioEvent, leadMuted: leadMuted);
        }
    }
}