using Core;
using UnityEngine;

public class QuackDisplay : MonoBehaviour {
    static PlayerService player {
        get => ServiceManager.Instance.GetService<PlayerService>();
    }

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