using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    // Singleton class
    public static Pop obj;
    public float livingTime = 0.5f;

    // Init the class
    void Awake() {
        obj = this;
    }

    // Function to show the object
    public void Show(Vector3 pos) {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    // Function to hide the object
    public void Dissapear() {
        gameObject.SetActive(false);
        Destroy(gameObject, livingTime);
    }

    // Destroy the class (only one instanciate)
    void OnDestroy() {
        obj = null;
    }
}
