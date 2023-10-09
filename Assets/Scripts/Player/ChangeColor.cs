using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Renderer")]
    [Tooltip("Rendu de la couleur du joueur")]
    private Renderer renderer;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////
    
    [Tooltip("Start")]    
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = GameManager.instance.Colors.PlayerMaterial;
    }

    [Tooltip("Update")]
    void Update()
    {
        if (GameManager.instance.Player.player.GetComponent<PlayerController>().GetInvincibility())
            ChangeBonusColor();
        else if (GameManager.instance.Player.player.GetComponent<PlayerController>().GetMalus())
            ChangeMalusColor();
        else
            ReturnOriginalColor();
    }

    [Tooltip("Retour à la couleur initiale (pour le véhicule)")]
    public void ReturnOriginalColor()
    {
        renderer.sharedMaterial = GameManager.instance.Colors.PlayerMaterial;
    }

    [Tooltip("Change la couleur initiale en couleur bonus (pour le véhicule)")]
    public void ChangeBonusColor()
    {
       renderer.sharedMaterial = GameManager.instance.Colors.PlayerMaterialBonus;
    }

    [Tooltip("Change la couleur initiale en couleur malus (pour le véhicule)")]
    public void ChangeMalusColor()
    {
        renderer.sharedMaterial = GameManager.instance.Colors.PlayerMaterialMalus;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
