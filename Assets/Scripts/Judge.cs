using System;
using Core;
using FMODUnity;
using Level;
using UnityEngine;
using UnityEngine.Serialization;

sealed class Judge : MonoBehaviour {
    static SongService songService {
        get => ServiceManager.Instance.GetService<SongService>();
    }

    static PlayerService player {
        get => ServiceManager.Instance.GetService<PlayerService>();
    }

    [SerializeField] internal bool isCorrect;

    [SerializeField] [ParamRef] string isCorrectParameter = string.Empty;

    void Update() {
        songService.SetSongTimeTo(Time.timeSinceLevelLoadAsDouble); // @TODO get from FMOD/sync with GameManager
        
        isCorrect = player.isQuacking == songService.shouldBeQuacking;

        RuntimeManager.StudioSystem.setParameterByName(isCorrectParameter, isCorrect ? 1 : 0);
    }

    [SerializeField] SongAsset song;

    void Start() {
        songService.PlaySong(song);
    }
}