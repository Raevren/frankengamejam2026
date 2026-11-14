using Core;
using UnityEngine;

sealed class SongService : IService {
    internal bool shouldBeQuacking {
        get => shouldBeQuackingCount > 0;
    }

    [SerializeField] internal int shouldBeQuackingCount;

    internal void StartQuack() => shouldBeQuackingCount++;

    internal void StopQuack() => shouldBeQuackingCount--;

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() { }
}