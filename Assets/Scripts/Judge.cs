using System;
using Core;
using FMODUnity;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

sealed class Judge : MonoBehaviour {
    static SongService song => ServiceManager.Instance.GetService<SongService>();

    static PlayerService player => ServiceManager.Instance.GetService<PlayerService>();

    [SerializeField] internal bool isCorrect;

    [SerializeField, ParamRef] string isCorrectParameter = string.Empty;

    void Update() {
        isCorrect = player.isQuacking == song.shouldBeQuacking;

        RuntimeManager.StudioSystem.setParameterByName(isCorrectParameter, isCorrect ? 1 : 0);
    }
}