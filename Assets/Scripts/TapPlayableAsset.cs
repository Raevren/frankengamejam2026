using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
sealed class TapPlayableAsset : PlayableAsset {
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) => ScriptPlayable<TapPlayableBehavior>.Create(graph);
}