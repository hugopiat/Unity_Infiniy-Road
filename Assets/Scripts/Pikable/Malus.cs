using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malus : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Joueur")]
    [Tooltip("Transform du player (position, rotation, taille)")]
    private GameObject player;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Start")]
    void Start()
    {
        player = GameManager.instance.Player.player;
    }

    [Tooltip("Test BoxCollider Trigger")]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiverMalus();
        }
    }
    private void ActiverMalus()
    {
        if (player.GetComponent<PlayerController>().GetMalus())
        {
            player.GetComponent<PlayerController>().SetMalus(false);
        }

        if (player.GetComponent<PlayerController>().GetInvincibility())
        {
            player.GetComponent<PlayerController>().SetInvincibility(false);
            player.GetComponent<PlayerController>().SetSpeed(player.GetComponent<PlayerController>().GetSpeed() / player.GetComponent<PlayerController>().CoefficientEvent);
        }

        player.GetComponent<PlayerController>().MalusFunction();
        Destroy(gameObject);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
