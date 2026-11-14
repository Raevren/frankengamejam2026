using Core;
using UnityEngine;

sealed class PlayerService : IService {
    [SerializeField]
    internal bool isQuacking;

    internal Vector3 quackPosition => quackRay.origin;
    [SerializeField]
    internal Ray quackRay;
    
    internal void StartQuack() {
        isQuacking = true;
    }

    internal void StopQuack()
    {
        isQuacking = false;
    }

    public void OnRegister() {}

    public void OnUnregister() {}

    public void OnAllRegistered() {}
}