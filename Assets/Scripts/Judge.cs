using Core;
using FMODUnity;
using UnityEngine;

sealed class Judge : MonoBehaviour {
    static SongService song {
        get => ServiceManager.Instance.GetService<SongService>();
    }

    static PlayerService player {
        get => ServiceManager.Instance.GetService<PlayerService>();
    }

    [SerializeField] internal bool isCorrect;

    [SerializeField] [ParamRef] string isCorrectParameter = string.Empty;

    void Update() {
        isCorrect = player.isQuacking == song.shouldBeQuacking;

        RuntimeManager.StudioSystem.setParameterByName(isCorrectParameter, isCorrect ? 1 : 0);
    }
}