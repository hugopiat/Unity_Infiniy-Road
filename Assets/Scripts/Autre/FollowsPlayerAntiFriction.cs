using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowsPlayerAntiFriction : MonoBehaviour
{

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Update")]
    void Update()
    {
        transform.position = new Vector3(GameManager.instance.Player.player.transform.position.x, transform.position.y, GameManager.instance.Player.player.transform.position.z);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
