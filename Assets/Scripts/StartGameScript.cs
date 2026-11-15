using System;
using System.Collections;
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
    
    

    bool isStarted = false;
    
    public void StartGame() {
        isStarted = true;
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
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        
        SceneManager.LoadScene("PascalStart", LoadSceneMode.Additive);
        yield return new WaitForSecondsRealtime(1);
        
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.UnloadSceneAsync("Loading");
    }

    public void Update() {
        if (isStarted) {
            return;
        }
        
        if(SceneManager.sceneCount > 1) {
            return;
        }
        
        if(SceneManager.GetActiveScene().name != "PascalStart") {
            return;
        }
        
        StartGame();
    }

    public void StartGameReally() => ServiceManager.Instance.GetService<SongService>().PlaySong(beginningMusic);
}