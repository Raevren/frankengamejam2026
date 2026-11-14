using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

sealed class TrackDisplay : MonoBehaviour {
    readonly struct TrackRowDisplay {
        internal readonly RectTransform transform;

        public TrackRowDisplay(TrackAsset asset, RectTransform transform, RectTransform prefab) {
            this.transform = transform;

            foreach (var clip in asset.GetClips()) {
                var clipTransform = Instantiate(prefab, transform);
                double start = clip.start;
                clipTransform.anchoredPosition = new Vector2((float)start, 0);
                clipTransform.sizeDelta = new Vector2((float)clip.duration, 1);
            }
        }
    }

    [SerializeField] PlayableDirector director;
    [SerializeField] RectTransform rowPrefab;
    [SerializeField] RectTransform quackPrefab;

    TrackRowDisplay[] rows = Array.Empty<TrackRowDisplay>();

    void Start() {
        if (director.playableAsset is not TimelineAsset timeline) {
            return;
        }

        rows = Enumerable
            .Range(0, timeline.rootTrackCount)
            .Select(timeline.GetRootTrack)
            .Select(track => new TrackRowDisplay(track, Instantiate(rowPrefab, transform), quackPrefab))
            .ToArray();
    }

    void Update() {
        if (transform is RectTransform rectTransform) {
            rectTransform.anchoredPosition = new Vector2((float)-director.time, 0);
        }
    }

    void OnDestroy() {
        foreach (var row in rows) {
            Destroy(row.transform.gameObject);
        }
    }
}