using Audio;
using Core;
using Level;
using UnityEngine;

public class StartGameScript : MonoBehaviour {
    [SerializeField] SongAsset beginningMusic;

    static readonly int open = Animator.StringToHash("open");

    [SerializeField] Animator _curtain;

    [SerializeField] GameObject _mainMenue;

    public void StartGame() {
        _mainMenue.SetActive(false);
        _curtain.SetTrigger(open);
    }

    public void StartGameReally() => ServiceManager.Instance.GetService<SongService>().PlaySong(beginningMusic);
}