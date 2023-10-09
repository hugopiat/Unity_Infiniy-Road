using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    //Param�tre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Cam�ra Main")]
    [Tooltip("Gameobject de la cam�ra vue de derri�re")]
    public GameObject camtps;

    [Header("Cam�ra Capot voiture")]
    [Tooltip("Gameobject de la cam�ra vue de devant la voiture")]
    public GameObject camfps;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
        if (Input.GetButtonDown("CameraView"))
        {
            ChangeCamera();
        }
    }

    public void ChangeCamera()
    {
        if (camtps.activeSelf)
        {
            camtps.SetActive(false);
            camtps.GetComponent<Camera>().enabled = false;
            camfps.SetActive(true);
            camfps.GetComponent<Camera>().enabled = true;
        }
        else
        {
            camfps.SetActive(false);
            camfps.GetComponent<Camera>().enabled = false;
            camtps.SetActive(true);
            camtps.GetComponent<Camera>().enabled = true;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
