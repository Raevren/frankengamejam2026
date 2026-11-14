using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level {
    [Serializable]
    public class Note {
        [SerializeField]
        public int note;
        [SerializeField]
        public int start;
        [SerializeField]
        public int duration;

        public Note(int note, int start, int duration) {
            this.note = note;
            this.start = start;
            this.duration = duration;
        }
    }
}