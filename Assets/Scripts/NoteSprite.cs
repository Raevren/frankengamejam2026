using System;
using System.Collections;
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

    private float noiseSeed;

    void Start()
    { 
        noiseSeed = Random.Range(0f, 100f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(noiseSeed, Time.unscaledTime * speed);
        
        float currentZ = Mathf.Lerp(minAngle, maxAngle, noise);
        
        transform.localRotation = Quaternion.Euler(0f, 0f, currentZ);
    }
}