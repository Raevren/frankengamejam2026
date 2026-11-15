using System;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

sealed class TrackDisplay : MonoBehaviour {
    readonly struct TrackRowDisplay {
        internal readonly RectTransform transform;

        public TrackRowDisplay(TrackAsset asset, RectTransform transform, RectTransform prefab) {
            this.transform = transform;

            foreach (var clip in asset.GetClips()) {
                if (clip.asset is TapPlayableAsset tap) {
                    var clipTransform = Instantiate(prefab, transform);
                    double start = clip.start;
                    clipTransform.anchoredPosition = new Vector2((float)start, 0);
                    if (clipTransform.TryGetComponent<NoteSprite>(out var sprite)) {
                        sprite.noteDuration = tap.noteDuration;
                    } else {
                        clipTransform.sizeDelta = new Vector2((float)clip.duration, 1);
                    }
                }
            }
        }
    }

    static SongService service => ServiceManager.Instance.GetService<SongService>();
    static PlayableDirector director => service.director;
    [SerializeField] RectTransform rowPrefab;
    [SerializeField] RectTransform quackPrefab;

    TrackRowDisplay[] rows = Array.Empty<TrackRowDisplay>();

    void OnEnable() {
        service.onPlay += StartSong;
        service.onUpdate += UpdateSong;
        service.onStop += StopSong;
    }

    void OnDisable() {
        service.onPlay -= StartSong;
        service.onUpdate -= UpdateSong;
        service.onStop -= StopSong;
    }

    void StartSong() {
        StopSong();

        if (director.playableAsset is not TimelineAsset timeline) {
            return;
        }

        rows = Enumerable
            .Range(0, timeline.rootTrackCount)
            .Select(timeline.GetRootTrack)
            .Select(track => new TrackRowDisplay(track, Instantiate(rowPrefab, transform), quackPrefab))
            .ToArray();
    }

    void UpdateSong(double time) {
        if (transform is RectTransform rectTransform) {
            rectTransform.anchoredPosition = new Vector2((float)-time, 0);
        }
    }

    void StopSong() {
        foreach (var row in rows) {
            Destroy(row.transform.gameObject);
        }

        rows = Array.Empty<TrackRowDisplay>();
    }
}