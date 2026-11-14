using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UObject = UnityEngine.Object;

sealed class TrackDisplay : MonoBehaviour {
    readonly struct TrackRowDisplay {
        internal readonly RectTransform transform;

        public TrackRowDisplay(TrackAsset asset, RectTransform transform, RectTransform prefab) {
            this.transform = transform;

            foreach (var clip in asset.GetClips()) {
                var clipTransform = Instantiate(prefab, transform);
                double start = clip.start;
                clipTransform.anchoredPosition = new((float)start, 0);
                clipTransform.sizeDelta = new((float)clip.duration, 1);
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
            rectTransform.anchoredPosition = new((float)-director.time, 0);
        }
    }

    void OnDestroy() {
        foreach (var row in rows) {
            Destroy(row.transform.gameObject);
        }
    }
}