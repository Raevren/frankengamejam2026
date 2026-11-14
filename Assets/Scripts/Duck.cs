using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Duck : MonoBehaviour {
        
    Animator animator;

    [SerializeField] SpriteRenderer duckTopRenderer;
    [SerializeField] SpriteRenderer duckBottomRenderer;
    [SerializeField] SpriteRenderer duckSleepRenderer;

    
    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(SetRandomSpeed());
    }

    IEnumerator SetRandomSpeed() {
        animator.speed =  Math.Min(1, Math.Max(0, Random.Range(0, 10)-7));
        yield return new WaitForSecondsRealtime(1);
        yield return SetRandomSpeed();
    }

    void Flip() {
        duckTopRenderer.flipX = !duckTopRenderer.flipX;
        duckBottomRenderer.flipX = !duckBottomRenderer.flipX;
        duckSleepRenderer.flipX = !duckSleepRenderer.flipX;
    }
    
    void ToggleSleep() {
        duckTopRenderer.enabled = !duckTopRenderer.enabled;
        duckBottomRenderer.enabled = !duckBottomRenderer.enabled;
        duckSleepRenderer.enabled = !duckSleepRenderer.enabled;
    }
}