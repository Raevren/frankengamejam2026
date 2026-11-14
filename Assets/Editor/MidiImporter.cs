using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Level;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Timeline;

namespace Editor {
    [ScriptedImporter(3, "mid")]
    sealed class MidiImporter : ScriptedImporter {
        public override void OnImportAsset(AssetImportContext ctx) {
            var asset = ScriptableObject.CreateInstance<SongAsset>();

            var file = new MidiFile(File.ReadAllBytes(ctx.assetPath));
            asset.ticksPerQuarterNote = file.TicksPerQuarterNote;
            asset.notes = ExtractNotes(file).ToArray();

            ctx.AddObjectToAsset("/", asset);

            var timeline = ScriptableObject.CreateInstance<TimelineAsset>();
            timeline.name = ctx.assetPath.Split("/")[^1];
            asset.timeline = timeline;
            ctx.AddObjectToAsset("/timeline", timeline);

            ctx.SetMainObject(asset);
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