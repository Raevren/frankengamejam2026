using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;

sealed class Judge : MonoBehaviour {
    [SerializeField] SongBehaviour song;

    [SerializeField] PlayerAsset player;

    [SerializeField] internal bool isCorrect;

    void Update() {
        isCorrect = player.isQuacking == song.shouldBeQuacking;
    }
}