using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio {
    [CreateAssetMenu]
    public class AudioConfig : ScriptableObject {
        [field: SerializeField] public EventReference QuackSound { get; private set; }
        [ParamRef] public string paramLeadVolume;

        [field: SerializeField]
        [field: ParamRef]
        public string ParamLTimedCorrectly { get; private set; }

        [field: SerializeField]
        [field: ParamRef]
        public string ParamLNoteHeight { get; private set; }
    }
}