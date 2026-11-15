using System;
using Audio;
using Core;
using FMOD.Studio;
using Level;
using UnityEngine;

sealed class PlayerService : IService {
    [SerializeField] internal bool isQuacking;
    EventInstance? _quackSoundEvent;
    FMODService fmodService = ServiceManager.Instance.GetService<FMODService>();

    internal Vector3 quackPosition {
        get => quackRay.origin;
    }

    [SerializeField] internal Ray quackRay;

    const int WRONG_NOTE_HEIGHT = 70;

    static SongService songService => ServiceManager.Instance.GetService<SongService>();

    internal void StartQuack() {
        isQuacking = true;

        if (songService.TryStartQuackingNow(out var note)) {
            fmodService.PlayQuack(true, note.note);
            onCorrectNote?.Invoke(note);
        } else {
            fmodService.PlayQuack(false, WRONG_NOTE_HEIGHT);
            onWrongNote?.Invoke();
        }
    }

    internal void StopQuack() {
        isQuacking = false;
        fmodService.ReleaseQuackSound();
    }

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() { }

    internal event Action<Note> onCorrectNote;
    internal event Action onWrongNote;
}