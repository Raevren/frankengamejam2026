using System;
using System.Collections;
using Core;
using Level;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

sealed class NoteSprite : MonoBehaviour {
    public ENoteDuration noteDuration {
        set {
            image.sprite = repository.GetRandomSprite(value);
        }
    }

    [SerializeField] Image image;

    [SerializeField] NoteRepository repository;

    [SerializeField] float speed = 1.5f;
    [SerializeField] float minAngle = -13f;
    [SerializeField] float maxAngle = 12f;
    [SerializeField] ParticleSystem correctNoteParticle; 

    private float noiseSeed;

    [SerializeField] public Note note;
    [SerializeField, Range(0, 1)] float onCorrectAlpha = 0.5f;

    void Start() {
        noiseSeed = Random.Range(0f, 100f);
    }


    static PlayerService playerService => ServiceManager.Instance.GetService<PlayerService>();

    void OnEnable() {
        playerService.onCorrectNote += OnCorrectNote;
    }

    void OnDisable() {
        playerService.onCorrectNote -= OnCorrectNote;
    }

    void OnCorrectNote(Note someNote) {
        if (note.start == someNote.start) {
            var color = image.color;
            color.a = onCorrectAlpha;
            image.color = color;
            var particle = Instantiate(correctNoteParticle, transform);
            particle.transform.position = transform.position;
        }
    }

    void Update() {
        float noise = Mathf.PerlinNoise(noiseSeed, Time.unscaledTime * speed);

        float currentZ = Mathf.Lerp(minAngle, maxAngle, noise);

        transform.localRotation = Quaternion.Euler(0f, 0f, currentZ);
    }
}