using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    // Singleton class
    public static FXManager obj;

    // Obtain the game object pop
    public GameObject pop;

    // Init the class
    void Awake() {
        obj = this;
    }

    // Show the pop game object within the same position about object init
    public void ShowPop(Vector3 pos) {
        pop.GetComponent<Pop>().Show(pos);
        GameObject myPop = Instantiate(pop, pos, Quaternion.identity);
        myPop.SetActive(true);
    }

    // Destroy the class (only one)
    void OnDestroy() {
        obj = null;    
    }
}
