using Core;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

sealed class PlayerService : IService {
    [SerializeField] internal bool isQuacking;
    EventInstance? _quackSoundEvent;

    internal Vector3 quackPosition {
        get => quackRay.origin;
    }

    [SerializeField] internal Ray quackRay;

    internal void StartQuack() {
        isQuacking = true;
        
        // TODO trigger sound
        //ReleaseQuackSound();
        //_quackSoundEvent = RuntimeManager.CreateInstance(Bootstrap.Instance.AudioConfig.quackSound);
        //_quackSoundEvent.Value.start();
    }

    internal void StopQuack() {
        isQuacking = false;
        //ReleaseQuackSound();
    }

    void ReleaseQuackSound() {
        if (_quackSoundEvent == null) {
            return;
        }

        _quackSoundEvent.Value.stop(STOP_MODE.ALLOWFADEOUT);
        _quackSoundEvent.Value.release();
        _quackSoundEvent = null;
    }

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() { }
}