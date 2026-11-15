using System;
using Core;
using FMODUnity;
using Level;
using UnityEngine;
using UnityEngine.Serialization;

sealed class DebugGameScript : MonoBehaviour {
    static SongService songService {
        get => ServiceManager.Instance.GetService<SongService>();
    }

    static PlayerService playerService {
        get => ServiceManager.Instance.GetService<PlayerService>();
    }

    [SerializeField] internal bool isCorrect;

    [SerializeField] internal Note lastCorrectNote;

    [SerializeField] internal Note currentCorrectNote;

    void OnEnable() {
        playerService.onCorrectNote += OnCorrectNote;
    }

    void OnDisable() {
        playerService.onCorrectNote -= OnCorrectNote;
    }

    void OnCorrectNote(Note n) {
        lastCorrectNote = n;
    }

    void Update() {
        isCorrect = playerService.isQuacking == songService.shouldBeQuacking;
        if (songService.TryStartQuackingNow(out var n)) {
            currentCorrectNote = n;
        }
    }

    [SerializeField] SongAsset song;

    void Start() {
        songService.PlaySong(song);
    }
}