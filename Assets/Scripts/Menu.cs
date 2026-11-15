using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Duck[] ducks;
 
    public static Menu Instance { get; private set; }
    
    void Start() {
        if (Instance != null) {
            Debug.LogError("[Menu]: Multiple instances of Menu exists!");
            DestroyImmediate(gameObject);
        }

        Instance = this;
        
       ducks = FindObjectsByType<Duck>(FindObjectsSortMode.None);
    }

    public void ShowCredits() {
        foreach (Duck duck in ducks) {
            duck.SayName();
        }
    }

    public void HideCredits() {
        foreach (Duck duck in ducks) {
            duck.SayMenu();
        }
    }
}
