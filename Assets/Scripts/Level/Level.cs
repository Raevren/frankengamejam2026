using UnityEditor;
using UnityEngine;

namespace Level {
    
    public class Level : MonoBehaviour {
        
        [SerializeField]
        private AudioClip _musicClip;

        [SerializeField] 
        private TextAsset _levelDefinition;
        
        public TextAsset LevelDefinition => _levelDefinition;
    }
}