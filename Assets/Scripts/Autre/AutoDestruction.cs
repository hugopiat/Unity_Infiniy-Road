using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    //Param�tre :

    //////////////////////////////////////////////////////////////////////////////////////////////////
    
    [Header("Cam�ra")]
    [Tooltip("GameObject de la cam�ra")]
    private GameObject Camera;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Start")]
    private void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    [Tooltip("Update")]
    void Update()
    {
        if(Camera.transform.position.z - 10.0f > transform.position.z || transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
