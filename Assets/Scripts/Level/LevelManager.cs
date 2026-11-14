using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MIDI;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Level {
    public class LevelManager : MonoBehaviour{
        void Start() {
            
            var noteMap = LoadLevel(GetComponent<Level>());
            Debug.unityLogger.Log(noteMap.Count);
            noteMap.ForEach(note => {
                Debug.Log($"Note: {note.note}, From: {note.from}, Duration: {note.duration}");
            });
        }

        public List<Note> LoadLevel(Level level) {

            var noteMap = new List<Note>();
            
            var midiTracks = new MidiFile(level.LevelDefinition.bytes);
            Debug.Log(midiTracks.Tracks.Length);
            var track = midiTracks.Tracks[1];

            track.MidiEvents.ForEach(action => {
                if (action.MidiEventType == MidiEventType.NoteOn) {
                    noteMap.Add(new Note(action.Note, action.Time, 0));
                }

                if (action.MidiEventType == MidiEventType.NoteOff) {
                    var note = noteMap.FindLast(n => n.duration == 0);
                    if (note != null) {
                        note.duration = action.Time - note.from;
                    }
                }
            });
            
            return noteMap;
        }
    
        private byte[] _ObjectToByteArray(object obj)
        {
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }    
    }
}