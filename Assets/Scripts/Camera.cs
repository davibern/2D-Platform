using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Get the transforms about the backgrounds
    public Transform bg0;
    public Transform bg1;
    public Transform bg2;

    // The speed about backgrouds
    public float factor0 = 1f;
    public float factor1 = 1 / 2f;
    public float factor2 = 1 / 4f;

    // Attributes to control de backgrounds
    private float displacement;
    private float iniCamPosFrame;
    private float nextCamPosFrame;

    // Update is called once per frame
    void Update() {
        // Obtain the position camera in this frame
        iniCamPosFrame = transform.position.x;
        transform.position = new Vector3(Player.obj.transform.position.x, transform.position.y, transform.position.z);
    }

    // Occurs after the last update
    void LateUpdate() {
        // Obtain the position camera after the update to have a substract.
        nextCamPosFrame = transform.position.x;
        // With this difference we will do the movement background
        bg0.position = new Vector3(bg0.position.x + (nextCamPosFrame - iniCamPosFrame) * factor0, bg0.position.y, bg0.position.z);
        bg1.position = new Vector3(bg1.position.x + (nextCamPosFrame - iniCamPosFrame) * factor1, bg1.position.y, bg1.position.z);
        bg2.position = new Vector3(bg2.position.x + (nextCamPosFrame - iniCamPosFrame) * factor2, bg2.position.y, bg0.position.z);
    }
}
