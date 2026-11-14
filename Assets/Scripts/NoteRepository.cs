using System;
using Level;
using UnityEngine;

[CreateAssetMenu]
sealed class NoteRepository : ScriptableObject {
    [SerializeField] Sprite[] fullNotes = Array.Empty<Sprite>();
    [SerializeField] Sprite[] halfNotes = Array.Empty<Sprite>();
    [SerializeField] Sprite[] quarterNotes = Array.Empty<Sprite>();
    [SerializeField] Sprite[] eighthNotes = Array.Empty<Sprite>();
    [SerializeField] Sprite[] sixteenthNotes = Array.Empty<Sprite>();

    public Sprite GetRandomSprite(ENoteDuration duration) {
        var notes = duration switch {
            ENoteDuration.FULL => fullNotes,
            ENoteDuration.HALF => halfNotes,
            ENoteDuration.QUARTER => quarterNotes,
            ENoteDuration.EIGHTH => eighthNotes,
            ENoteDuration.SIXTEENTH => sixteenthNotes,
            _ => throw new ArgumentOutOfRangeException(nameof(duration), duration, null)
        };
        return GetRandom(notes);
    }

    static T GetRandom<T>(T[] items) {
        return items[UnityEngine.Random.Range(0, items.Length)];
    }
}