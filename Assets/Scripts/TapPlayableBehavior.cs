using Core;
using UnityEngine;
using UnityEngine.Playables;

sealed class TapPlayableBehavior : PlayableBehaviour {
    SongService song => ServiceManager.Instance.GetService<SongService>();

    bool isQuacking;

    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        if (!isQuacking) {
            isQuacking = true;
            song.StartQuack();
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info) {
        if (isQuacking) {
            isQuacking = false;
            song.StopQuack();
        }
    }
}