using Core;
using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio {
    public class BgmPlayer : IService {
        private EventInstance? _eventInstance;

        /// <summary>
        /// Starts a new track
        /// </summary>
        /// <param name="reference">The FMOD event</param>
        /// <param name="stopOtherImmediately">If another track is playing, this sets if it ends immediately or fades out</param>
        public void Play(EventReference reference, bool stopOtherImmediately = false) {
            Stop(stopOtherImmediately);
            _eventInstance = RuntimeManager.CreateInstance(reference);
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
        
        #region IService Implementation
        public void OnRegister() { }

        public void OnUnregister() { }

        public void OnAllRegistered() { }
        #endregion
    }
}