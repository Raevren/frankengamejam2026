using System;
using Core;
using Level;
using UnityEngine;

public class QuackDisplay : MonoBehaviour {
    static PlayerService player {
        get => ServiceManager.Instance.GetService<PlayerService>();
    }

    [SerializeField] GameObject quackPrefab;
    [SerializeField] ParticleSystem wrongNoteParticles;
    [SerializeField] ParticleSystem niceNoteParticles;

    GameObject quackInstance;

    void Start() {
        player.onWrongNote += DisplayWrongNote;
        player.onCorrectNote += DisplayNiceNote;
    }
    
    void DisplayWrongNote() {
        var par = Instantiate(wrongNoteParticles, transform);
        par.transform.position = player.quackPosition;
    }
    
    void DisplayNiceNote(Note note) {
        var par = Instantiate(niceNoteParticles, transform);
        par.transform.position = player.quackPosition;
    }

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