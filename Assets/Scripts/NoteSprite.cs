using Level;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

sealed class NoteSprite : MonoBehaviour {
    public ENoteDuration noteDuration {
        set {
            image.sprite = repository.GetRandomSprite(value);
        }
    }

    [SerializeField] Image image;

    [SerializeField] NoteRepository repository;
}