using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace Level {
    public class SongAsset : ScriptableObject {
        [Header("Sub-Assets")]
        [SerializeField] public TimelineAsset timeline;
        
        [Header("MIDI file")]
        [SerializeField] public int ticksPerQuarterNote;
        [SerializeField] public Note[] notes = Array.Empty<Note>();
    }
}