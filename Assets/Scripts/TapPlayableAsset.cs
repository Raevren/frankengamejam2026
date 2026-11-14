using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
sealed class TapPlayableAsset : PlayableAsset {
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<TapPlayableBehavior>.Create(graph);
        playable.GetBehaviour().song = owner.GetComponent<SongBehaviour>();
        return playable;
    }
}