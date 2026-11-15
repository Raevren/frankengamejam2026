using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Duck : MonoBehaviour {
        
    Animator animator;

    [SerializeField] SpriteRenderer duckTopActiveRenderer;
    [SerializeField] SpriteRenderer duckTopIdleRenderer;
    [SerializeField] SpriteRenderer duckBottomRenderer;
    
    [SerializeField] SpriteRenderer duckSleepRenderer;

    
    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(SetRandomSpeed());
        if (!gameObject.CompareTag("Dirigente") && Random.Range(0, 2) == 1) {
            Flip();
        }
    }

    IEnumerator SetRandomSpeed() {
        var wait = new WaitForSecondsRealtime(1);
        while (true) {
            animator.speed = Mathf.Clamp(Random.Range(0, 10) - 7, 0, 1);
            yield return wait;    
        }
    }

    #region The Looks!
    
    void Flip() {
        duckTopIdleRenderer.flipX = !duckTopIdleRenderer.flipX;
        duckTopActiveRenderer.flipX = !duckTopActiveRenderer.flipX;
        duckBottomRenderer.flipX = !duckBottomRenderer.flipX;
        duckSleepRenderer.flipX = !duckSleepRenderer.flipX;
    }
    
    void Sleep() {
        duckTopIdleRenderer.enabled = false;
        duckTopActiveRenderer.enabled = false;
        duckBottomRenderer.enabled = false;
        duckSleepRenderer.enabled = true;
    }
    
    void Idle() {
        duckTopIdleRenderer.enabled = true;
        duckTopActiveRenderer.enabled = false;
        duckBottomRenderer.enabled = true;
        duckSleepRenderer.enabled = false;
    }
    
    void Active() {
        duckTopIdleRenderer.enabled = false;
        duckTopActiveRenderer.enabled = true;
        duckBottomRenderer.enabled = true;
        duckSleepRenderer.enabled = false;
    }
    
    #endregion
    
    
}