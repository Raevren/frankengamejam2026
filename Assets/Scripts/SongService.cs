using System;
using Core;
using Level;
using UnityEngine;
using UnityEngine.Playables;

sealed class SongService : MonoBehaviour, IService {
    internal bool shouldBeQuacking {
        get => shouldBeQuackingCount > 0;
    }

    [SerializeField] internal PlayableDirector director;

    [SerializeField] internal int shouldBeQuackingCount;

    internal void StartQuack() => shouldBeQuackingCount++;

    internal void StopQuack() => shouldBeQuackingCount--;

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() { }

    public void PlaySong(SongAsset song) {
        director.playableAsset = song.timeline;
        director.time = 0;
        director.Evaluate();
        
        onPlay?.Invoke();
    }

    public void SetSongTimeTo(double time) {
        director.time = time;
        director.Evaluate();
        
        onUpdate?.Invoke();
    }

    public void StopSong() {
        onStop?.Invoke();
    }

    public event Action onPlay;
    public event Action onUpdate;
    public event Action onStop;
}