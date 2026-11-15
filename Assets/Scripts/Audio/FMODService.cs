using System;
using Core;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio {
    public class FMODService : IService {
        public static event Action<EventInstance> OnBgmEnded;
        readonly AudioConfig _config;
        static EventInstance? _musicEventInstance, _quackEventInstance;

        public FMODService(AudioConfig config) {
            _config = config;
        }

        /// <summary>
        /// Starts a new track
        /// </summary>
        /// <param name="reference">The FMOD event</param>
        /// <param name="stopOtherImmediately">If another track is playing, this sets if it ends immediately or fades out</param>
        /// <param name="leadMuted">If true, the lead tracks are muted</param>
        public void PlayBGM(EventReference reference, bool stopOtherImmediately = false, bool leadMuted = false) {
            StopBGM(stopOtherImmediately);
            _musicEventInstance = RuntimeManager.CreateInstance(reference);
            ToggleBGMLeadMute(leadMuted);
            _musicEventInstance.Value.start();
            _musicEventInstance.Value.setCallback(BgmEventCallback);
        }

        /// <summary>
        /// Stops the currently playing track
        /// </summary>
        /// <param name="immediate">If false, smooth fade out is applied</param>
        public void StopBGM(bool immediate = true) {
            if (_musicEventInstance == null) {
                return;
            }

            _musicEventInstance.Value.stop(immediate ? STOP_MODE.ALLOWFADEOUT : STOP_MODE.IMMEDIATE);
            _musicEventInstance.Value.release();
            _musicEventInstance.Value.setCallback(null);
            _musicEventInstance = null;
        }

        /// <summary>
        /// Mutes/Unmutes the lead melody
        /// </summary>
        public void ToggleBGMLeadMute(bool mute) {
            if (_musicEventInstance == null) {
                Debug.LogWarning("[MusicService] Cannot mute lead tracks, no track is playing");
                return;
            }

            RuntimeManager.StudioSystem.setParameterByName(_config.paramLeadVolume, mute ? 0 : 1);
        }

        [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
        static RESULT BgmEventCallback(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr) {
            if (_musicEventInstance != null && type == EVENT_CALLBACK_TYPE.STOPPED) {
                OnBgmEnded?.Invoke(_musicEventInstance.Value);
                Debug.Log("[MusicService] End of BGM");
            }
            return RESULT.OK;
        }

        public void PlayQuack(bool isCorrect, int noteHeight) {
            ReleaseQuackSound();
            _quackEventInstance = RuntimeManager.CreateInstance(_config.QuackSound);
            _quackEventInstance.Value.setParameterByName(_config.ParamLTimedCorrectly, isCorrect ? 1 : 0);
            _quackEventInstance.Value.setParameterByName(_config.ParamLNoteHeight, noteHeight);
            _quackEventInstance.Value.start();
        }

        public void ReleaseQuackSound() {
            if (_quackEventInstance == null) {
                return;
            }

            _quackEventInstance.Value.stop(STOP_MODE.ALLOWFADEOUT);
            _quackEventInstance.Value.release();
            _quackEventInstance = null;
        }

        #region IService Implementation

        public void OnRegister() { }

        public void OnUnregister() { }

        public void OnAllRegistered() { }

        #endregion

        public bool TryGetTime(out double time) {
            if (!_musicEventInstance.HasValue) {
                time = 0;
                return false;
            }

            var result = _musicEventInstance.Value.getTimelinePosition(out int timeMilliseconds);
            time = timeMilliseconds * 0.001;
            return result == RESULT.OK;
        }
    }
}