using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Duck[] ducks;
    void Start() {
       ducks = GameObject.FindObjectsByType<Duck>(FindObjectsSortMode.None);
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
