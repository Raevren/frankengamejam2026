using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.Timeline;

namespace Level {
    public class SongAsset : ScriptableObject {
        [Header("Sub-Assets")] [SerializeField]
        public TimelineAsset timeline;

        [SerializeField] public EventReference audioEvent;

        [Header("MIDI file")] [SerializeField] public int ticksPerNote;

        public double ticksPerSecond => (ticksPerNote * beatsPerMinute) / (double)240;
        public double secondsPerTick => 1 / ticksPerSecond;

        [SerializeField] public int beatsPerMinute;
        [SerializeField] public Note[] notes = Array.Empty<Note>();
    }
}