using Audio;
using Core;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

sealed class PlayerService : IService {
    [SerializeField] internal bool isQuacking;
    EventInstance? _quackSoundEvent;
    FMODService fmodService = ServiceManager.Instance.GetService<FMODService>();

    internal Vector3 quackPosition {
        get => quackRay.origin;
    }

    [SerializeField] internal Ray quackRay;

    internal void StartQuack() {
        isQuacking = true;
        fmodService.PlayQuack(true, 70);
    }

    internal void StopQuack() {
        isQuacking = false;
        fmodService.ReleaseQuackSound();
    }

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() { }
}