using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Duck : MonoBehaviour {
        
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(SetRandomSpeed());
    }

    IEnumerator SetRandomSpeed() {
        animator.speed =  Math.Min(1, Math.Max(0, Random.Range(0, 10)-7));
        yield return new WaitForSecondsRealtime(1);
        yield return SetRandomSpeed();
    }
}