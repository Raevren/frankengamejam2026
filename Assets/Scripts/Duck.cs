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
            animator.speed =  random.Next(0, 2);
            yield return new WaitForSecondsRealtime(random.Next(2, 10));
        }
    }
}