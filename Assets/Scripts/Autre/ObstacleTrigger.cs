using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Joueur")]
    [Tooltip("Transform du player (position, rotation, taille)")]
    private GameObject player;
    private bool Premiere_Collision;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Start")]
    void Start()
    {
        player = GameManager.instance.Player.player;
        Premiere_Collision = false; //boolean pour etre sur que la scene looser se load que une fois
    }

    [Tooltip("Premiere_Collision BoxCollider Trigger")]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            testCollisionFirst();
        }
    }

    private void testCollisionFirst()
    {
        if (!player.GetComponent<PlayerController>().GetInvincibility() && !Premiere_Collision)
        {
            GameManager.instance.UnloadLevel(GameManager.instance._current_level_name);
            GameManager.instance.LoadLevel("Looser");
            Premiere_Collision = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
