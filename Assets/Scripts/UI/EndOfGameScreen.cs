using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace UI {
    public class EndOfGameScreen : MonoBehaviour {

        [SerializeField] TextMeshProUGUI scoreText;

        public void SetScore(int score) {
            StopAllCoroutines();
            StartCoroutine(CountScoreUp(score));
        }

        public void BackToTitle() {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }

        IEnumerator CountScoreUp(int endScore) {
            for (int repeat = 0; repeat < 10; repeat++) {
                scoreText.text = Random.Range(1000, 9999).ToString();
                yield return new WaitForSeconds(0.1f);
            }
            scoreText.text = endScore.ToString();
        }
    }
}
