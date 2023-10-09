using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LirePositionFantome : MonoBehaviour
{
    [SerializeField] private InfoChronoJoueur ChronoJoueur;
    public GameObject JsonObject;
    public double MaxTempsChrono;
    private int indexPosition = 0; 
    private int indexRotation = 0;

    private void Start()
    {
        if (LireChrono() && GameManager.instance.Player.player.GetComponent<Chrono>().enabled)
        {
            InvokeRepeating("LirePosition", 0f, 0.01f);
            InvokeRepeating("LireRotation", 0f, 0.01f);
        }
        if (!GameManager.instance.Player.player.GetComponent<Chrono>().enabled)
            JsonObject.SetActive(false);
    }

    private bool LireChrono()
    {
        try
        {
            string meilleurChronoStr = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/MeilleursChronosJoueurs.json");
            ChronoJoueur = JsonUtility.FromJson<InfoChronoJoueur>(meilleurChronoStr);
            MaxTempsChrono = ChronoJoueur.tempsChrono;
            return true;
        }
        catch (Exception e)
        {
            print("Erreur sur fichier json (" + Application.streamingAssetsPath + "/MeilleursChronosJoueurs.json" + ")! ! !");
            print("\n err = " + e.Message);
            return false;
        }
    }

    private void LirePosition()
    {
        if(indexPosition < ChronoJoueur.positions.Count)
        {
            transform.position = ChronoJoueur.positions[indexPosition].position;
            indexPosition++;
        }
    }

    private void LireRotation()
    {
        if (indexRotation < ChronoJoueur.rotation.Count)
        {
            transform.rotation = Quaternion.Euler(ChronoJoueur.rotation[indexRotation].rotation);
            indexRotation++;
        }
    }
}
