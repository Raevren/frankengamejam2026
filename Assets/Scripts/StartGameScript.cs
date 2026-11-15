using Audio;
using Core;
using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {
    [SerializeField] SongAsset beginningMusic;

    static readonly int open = Animator.StringToHash("open");
    static readonly int close = Animator.StringToHash("close");

    [SerializeField] Animator _curtain;

    public void StartGame() {
        _curtain.SetTrigger(open);
    }

    public void LoadTutorial() 
    {
        StartCoroutine(LoadTutorialRoutine());
    }

    IEnumerator LoadTutorialRoutine() 
    {
        _curtain.SetTrigger(close);
        yield return null; 
        while (_curtain.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || _curtain.IsInTransition(0)) 
        {
            yield return null;
        }
        SceneManager.LoadScene("PascalStart", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Menu");
    }
    
    public void StartGameReally() => ServiceManager.Instance.GetService<SongService>().PlaySong(beginningMusic);
}