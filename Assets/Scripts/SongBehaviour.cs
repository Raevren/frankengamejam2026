using UnityEngine;

sealed class SongBehaviour : MonoBehaviour {
    internal bool shouldBeQuacking => shouldBeQuackingCount > 0;

    [SerializeField] internal int shouldBeQuackingCount;

    internal void StartQuack() {
        shouldBeQuackingCount++;
    }

    internal void StopQuack() {
        shouldBeQuackingCount--;
    }
}