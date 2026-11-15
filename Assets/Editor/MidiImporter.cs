using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FMODUnity;
using Level;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Timeline;

namespace Editor {
    [ScriptedImporter(4, "mid")]
    sealed class MidiImporter : ScriptedImporter {
        [SerializeField] int beatsPerMinute = 103;
        [SerializeField]
        public EventReference audioEvent;

        public override void OnImportAsset(AssetImportContext ctx) {
            var asset = ScriptableObject.CreateInstance<SongAsset>();

            var file = new MidiFile(File.ReadAllBytes(ctx.assetPath));
            asset.audioEvent = audioEvent;
            asset.beatsPerMinute = beatsPerMinute;
            asset.ticksPerNote = 4 * file.TicksPerQuarterNote;
            asset.notes = ExtractNotes(file).ToArray();

            var tracks = new Dictionary<int, List<Note>>();
            foreach (var note in asset.notes) {
                if (!tracks.TryGetValue(note.note, out var list)) {
                    list = tracks[note.note] = new List<Note>();
                }

                list.Add(note);
            }

            ctx.AddObjectToAsset("/", asset);

            var timeline = ScriptableObject.CreateInstance<TimelineAsset>();
            timeline.name = ctx.assetPath.Split("/")[^1];
            asset.timeline = timeline;

            double secondsPerTick = (double)240 / (asset.ticksPerNote * beatsPerMinute);
            foreach (int key in Enumerable.Range(tracks.Keys.Min(), 1 + tracks.Keys.Max() - tracks.Keys.Min()).Reverse()) {
                var track = timeline.CreateTrack<TapTrack>();
                track.name = $"Note {key}";

                if (tracks.TryGetValue(key, out var notes)) {
                    foreach (var note in notes) {
                        var clip = track.CreateClip<TapPlayableAsset>();
                        clip.start = note.start * secondsPerTick;
                        clip.duration = note.duration * secondsPerTick;
                        if (clip.asset is TapPlayableAsset tap) {
                            tap.noteDuration = CalculateNoteDuration(note.duration, asset.ticksPerNote);
                        }

                        ctx.AddObjectToAsset($"/timeline/{key}/{note.start}", clip.asset);
                    }
                }

                ctx.AddObjectToAsset($"/timeline/{key}", track);
            }

            ctx.AddObjectToAsset("/timeline", timeline);

            ctx.SetMainObject(asset);
        }

        static ENoteDuration CalculateNoteDuration(int noteDuration, int ticksPerNote) {
            return (ENoteDuration)(ticksPerNote / noteDuration);
        }

        static IEnumerable<Note> ExtractNotes(MidiFile file) {
            foreach (var track in file.Tracks) {
                var noteMap = new List<Note>();

                foreach (var action in track.MidiEvents) {
                    switch (action.MidiEventType) {
                        case MidiEventType.NoteOn:
                            noteMap.Add(new Note(action.Note, action.Time, 0));
                            break;
                        case MidiEventType.NoteOff:
                            if (noteMap.FindLast(n => n.duration == 0) is { } note) {
                                noteMap.Remove(note);
                                note.duration = action.Time - note.start;
                                yield return note;
                            }

                            break;
                        case MidiEventType.MetaEvent:
                        case MidiEventType.ControlChange:
                        case MidiEventType.PitchBendChange:
                        case MidiEventType.ProgramChange:
                            break;
                        case MidiEventType.KeyAfterTouch:
                        case MidiEventType.ChannelAfterTouch:
                        default:
                            throw new ArgumentOutOfRangeException(action.MidiEventType.ToString());
                    }
                }
            }
        }
    }
}