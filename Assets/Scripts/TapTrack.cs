using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(TapPlayableAsset))]
public class TapTrack : TrackAsset {
    protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip) {
        var playable = base.CreatePlayable(graph, gameObject, clip);
        return playable;
    }
}