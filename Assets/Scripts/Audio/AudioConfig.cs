using FMODUnity;
using UnityEngine;

namespace Audio {
    [CreateAssetMenu]
    public class AudioConfig : ScriptableObject {
        [field: SerializeField] public EventReference quackSound { get; private set; }
    }
}
