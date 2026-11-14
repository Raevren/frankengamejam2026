using System;
using UnityEngine;
using UnityEngine.InputSystem;

sealed class PlayerInputHandler : MonoBehaviour {
    InputSystem_Actions actions;

    [SerializeField] PlayerAsset player;
    [SerializeField] Camera mainCamera;

    void Awake() {
        actions = new InputSystem_Actions();

        actions.Player.Attack.started += _ => player.StartQuack();
        actions.Player.Attack.canceled += _ => player.StopQuack();
    }

    void OnEnable() {
        actions.Enable();
    }

    void Update() {
        if (actions.Player.Attack.inProgress) {
            player.quackRay = mainCamera.ScreenPointToRay(Pointer.current.position.value);
        }
    }

    void OnDisable() {
        actions.Disable();
    }

    void OnDestroy() {
        actions.Dispose();
    }
}