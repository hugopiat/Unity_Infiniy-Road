using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIfRoadIsUnder : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("AntiFriction avec la route")]
    [Tooltip("GameObject qui permet d'éviter au player de se cogner contre la route qui se créé petit à petit")]
    public GameObject AntiFrictionRoad;

    [Header("Player")]
    [Tooltip("GameObject du player")]
    private GameObject player;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Update : " + "Test de collision avec Raycast et un layer ('Road')")]
    private void Update()
    {
        Test();
    }

    private void Test()
    {
        if (!Physics.Raycast(GameManager.instance.Player.player.transform.position, Vector3.down, 5f, 1 << 6))
        {
            AntiFrictionRoad.GetComponent<BoxCollider>().enabled = false;
        }
        else
            AntiFrictionRoad.GetComponent<BoxCollider>().enabled = true;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
