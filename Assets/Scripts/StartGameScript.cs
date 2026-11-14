using Audio;
using Core;
using FMODUnity;
using UnityEngine;

public class StartGameScript : MonoBehaviour {

    [SerializeField] EventReference beginningMusic;
    
    static readonly int open = Animator.StringToHash("open");

    [SerializeField]
    private Animator _curtain;
    
   [SerializeField] 
    private GameObject _mainMenue;

    public void StartGame() {
        _mainMenue.SetActive(false);
        _curtain.SetTrigger(open);
    }

    public void StartGameReally() {
        ServiceManager.Instance.GetService<MusicService>().Play(beginningMusic);
    }
}
