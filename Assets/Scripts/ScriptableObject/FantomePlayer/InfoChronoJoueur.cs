using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InfoChronoJoueur
{
    ~InfoChronoJoueur()
    {
        positions.Clear();
        rotation.Clear();
    }

    public string Pseudo; // l'utilisateur
    public string JourHeureChronoStr; // le jour et heur du chrono
    public double tempsChrono; // temps en secondes
    public List<PositionChrono> positions = new List<PositionChrono>(); // les positions intermédiaires
    public List<RotationChrono> rotation = new List<RotationChrono>(); // les positions intermédiaires
}

[System.Serializable]
public class SauvegardeChrono
{
    [SerializeField] private InfoChronoJoueur ChronoJoueur;
    public double MaxTempsChrono;

    public void Start()
    {
        string meilleurChronoStr = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/MeilleursChronosJoueurs.json");
        ChronoJoueur = JsonUtility.FromJson<InfoChronoJoueur>(meilleurChronoStr);
        MaxTempsChrono = ChronoJoueur.tempsChrono;
    }

    public void SauvegarderChrono(InfoChronoJoueur infoChronoJoueur)
    {
        Start();
        ChronoJoueur = infoChronoJoueur; // sera mis à jour

        if (ChronoJoueur.tempsChrono < MaxTempsChrono)
        {
            string saveChronoJoueur = JsonUtility.ToJson(ChronoJoueur);
            System.IO.File.WriteAllText(Application.streamingAssetsPath + "/MeilleursChronosJoueurs.json", saveChronoJoueur);
        }
    }
}


[System.Serializable]
public class PositionChrono
{
    public double tempsEcoule;
    public Vector3 position;
    public PositionChrono(double _chrono, Vector3 _pos)
    {
        tempsEcoule = _chrono;
        position = _pos;
    }
}

[System.Serializable]
public class RotationChrono
{
    public double tempsEcoule;
    public Vector3 rotation;
    public RotationChrono(double _chrono, Vector3 _rot)
    {
        tempsEcoule = _chrono;
        rotation = _rot;
    }
}



