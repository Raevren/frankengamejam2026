using Core;

sealed class ScoreService : IService {
    static PlayerService playerService => ServiceManager.Instance.GetService<PlayerService>();
    static SongService songService => ServiceManager.Instance.GetService<SongService>();

    public int correctNotes = 0;
    public int wrongNotes = 0;

    public void OnRegister() { }

    public void OnUnregister() { }

    public void OnAllRegistered() {
        playerService.onCorrectNote += _ => correctNotes++;
        playerService.onWrongNote += () => wrongNotes++;
        songService.onPlay += () => {
            correctNotes = 0;
            wrongNotes = 0;
        };
    }
}