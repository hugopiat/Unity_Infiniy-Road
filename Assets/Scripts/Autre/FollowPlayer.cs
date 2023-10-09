using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Joueur")]
    [Tooltip("GameObject du player")]
    private GameObject player;

    [Header("Position Caméra Main")]
    [Tooltip("Position de la caméra par rapport au player (vue de derrière")]
    private Vector3 Offset_Camera = new Vector3(0f, 7.5f, -10);

    [Header("Position Décors")]
    [Tooltip("Position du décors par rapport au player")]
    private Vector3 Offset_Environment = new Vector3(0f, -60f, 20f);

    [Header("Position Caméra Capot")]
    [Tooltip("Position de la caméra par rapport au player (vue de devant la voiture")]
    private Vector3 Offset_CameraCapot = new Vector3(0f, 1.6f, 2.25f);

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Start")]
    private void Start()
    {
        player = GameManager.instance.Player.player;
    }

    [Tooltip("LateUpdate")]
    void LateUpdate()
    {
        Position();
    }

    private void Position()
    {
        if (CompareTag("MainCamera"))
            transform.position = player.transform.position + Offset_Camera;

        if (CompareTag("Environment"))
            transform.position = player.transform.position + Offset_Environment;

        if (CompareTag("CamCapot"))
        {
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
            transform.position += transform.rotation * Offset_CameraCapot;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
