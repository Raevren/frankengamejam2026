using System;
using Core;
using FMODUnity;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

sealed class Judge : MonoBehaviour {
    static SongService song => ServiceManager.Instance.GetService<SongService>();

    static PlayerService player => ServiceManager.Instance.GetService<PlayerService>();

    [SerializeField] internal bool isCorrect;

    [SerializeField] ParamRef isCorrectParameter;

    void Update() {
        isCorrect = player.isQuacking == song.shouldBeQuacking;

        RuntimeManager.StudioSystem.setParameterByName(isCorrectParameter.Name, isCorrect ? 1 : 0);
    }
}