using UnityEngine;

public class Curtain : MonoBehaviour
{

    [SerializeField]
    private StartGameScript _startGameScript;
    
    public void FinishOpen() {
        _startGameScript.StartGameReally();
    }
    
}
