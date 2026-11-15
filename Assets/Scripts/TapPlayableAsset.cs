using System;
using Level;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public sealed class TapPlayableAsset : PlayableAsset {
    [SerializeField] public ENoteDuration noteDuration = ENoteDuration.QUARTER;
    [SerializeField] public Note note;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<TapPlayableBehavior>.Create(graph);
        return playable;
    }
}