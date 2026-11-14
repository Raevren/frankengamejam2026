using Core;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio {
    public class MusicService : IService {
        
        EventInstance? _eventInstance;
        const string PARAM_LEAD_VOLUME = "LeadVolume";

        /// <summary>
        /// Starts a new track
        /// </summary>
        /// <param name="reference">The FMOD event</param>
        /// <param name="stopOtherImmediately">If another track is playing, this sets if it ends immediately or fades out</param>
        /// <param name="leadMuted">If true, the lead tracks are muted</param>
        public void Play(EventReference reference, bool stopOtherImmediately = false, bool leadMuted = false) {
            Stop(stopOtherImmediately);
            _eventInstance = RuntimeManager.CreateInstance(reference);
            ToggleLeadMute(leadMuted);
            _eventInstance.Value.start();
        }

        /// <summary>
        /// Stops the currently playing track
        /// </summary>
        /// <param name="immediate">If false, smooth fade out is applied</param>
        public void Stop(bool immediate = true) {
            if (_eventInstance == null) {
                return;
            }
            _eventInstance.Value.stop(immediate ? STOP_MODE.ALLOWFADEOUT : STOP_MODE.IMMEDIATE);
            _eventInstance.Value.release();
            _eventInstance = null;
        }

        /// <summary>
        /// Mutes/Unmutes the lead melody
        /// </summary>
        public void ToggleLeadMute(bool mute) {
            if (_eventInstance == null) {
                Debug.LogWarning("[MusicService] Cannot mute lead tracks, no track is playing");
                return;
            }
            RuntimeManager.StudioSystem.setParameterByName(PARAM_LEAD_VOLUME, mute ? 0 : 1);
        }
        
        #region IService Implementation
        public void OnRegister() { }

        public void OnUnregister() { }

        public void OnAllRegistered() { }
        #endregion
    }
}