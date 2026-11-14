using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lamp : MonoBehaviour {
    
    [SerializeField] 
    private float maxZ = 0.5f;
    
    [SerializeField]
    private float minZ = -0.5f;

    [SerializeField]
    private float swingSpeed = 10f; 
    private float direction = 0f;
    private float currentZ = 0f;

    void Start() {
        currentZ = transform.localEulerAngles.z;
        if (currentZ > 180f) {
            currentZ -= 360f;
        }
    }

    void Update() {
        direction = RollNextDirection();
        
        var deltaRotation = direction * swingSpeed * Time.deltaTime;

        currentZ = Mathf.Clamp(currentZ + deltaRotation, minZ, maxZ);
        
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x, 
            transform.localEulerAngles.y, 
            currentZ
        );
    }

    float RollNextDirection() {
        var nextDirection = Random.Range(-1f, 1f) * 0.1f + direction;
        return Mathf.Clamp(nextDirection, -1f, 1f);
    }
}
