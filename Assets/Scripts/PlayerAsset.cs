using UnityEngine;

[CreateAssetMenu]
sealed class PlayerAsset : ScriptableObject {
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
}