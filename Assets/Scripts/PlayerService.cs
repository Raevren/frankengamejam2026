using Core;
using UnityEngine;

sealed class PlayerService : IService {
    [SerializeField] internal bool isQuacking;

    internal Vector3 quackPosition {
        get => quackRay.origin;
    }

    [SerializeField] internal Ray quackRay;

    static SongService songService => ServiceManager.Instance.GetService<SongService>();
    
    bool isCorrect => songService.shouldStartQuackingNow;

    internal void StartQuack() => isQuacking = true;

    internal void StopQuack() => isQuacking = false;

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() { }
}