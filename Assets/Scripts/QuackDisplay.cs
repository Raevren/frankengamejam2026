using System;
using UnityEngine;

public class QuackDisplay : MonoBehaviour {
    [SerializeField] PlayerAsset player;
    [SerializeField] GameObject quackPrefab;

    GameObject quackInstance;

    void Update() {
        bool doesExist = quackInstance;
        bool shouldExist = player.isQuacking;

        if (doesExist != shouldExist) {
            if (shouldExist) {
                quackInstance = Instantiate(quackPrefab, transform);
            } else {
                Destroy(quackInstance);
                quackInstance = null;
            }
        }

        if (quackInstance) {
            quackInstance.transform.position = player.quackPosition;
        }
    }
}