using System.Collections;
using UnityEngine;

namespace System.Runtime.CompilerServices {
    public class Duck : MonoBehaviour {
        
        Animator animator;

        void Start() {
            animator = GetComponent<Animator>();
            StartCoroutine(SetRandomSpeed());
        }

        IEnumerator SetRandomSpeed() {
            var random = new Random();
            animator.speed =  Math.Min(1, Math.Max(0, random.Next(0, 10)-7));
            yield return new WaitForSecondsRealtime(1);
            yield return SetRandomSpeed();
        }
    }
}