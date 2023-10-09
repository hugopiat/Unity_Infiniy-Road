using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Caméra Main")]
    [Tooltip("Gameobject de la caméra vue de derrière")]
    public GameObject camtps;

    [Header("Caméra Capot voiture")]
    [Tooltip("Gameobject de la caméra vue de devant la voiture")]
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
