using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CPlayer
{
    [Header("Joueur")]
    [Tooltip("Transform du player (position, rotation, taille)")]
    public GameObject player;

    [Header("Distance")]
    [Tooltip("Distance parcourue du joueur")]
    public int Distance;

    [Header("Propriétés vitesse joueur")]
    [Tooltip("Vitesse du player")]
    public float Speed = 20f;
    [Tooltip("Vitesse pour tourner")]
    public float TurnSpeed = 1.5f;

    [Header("Vitesse Propriétés")]
    [Tooltip("Maximum vitesse du player")]
    public float MaxSpeed = 35f;
    [Tooltip("Minimum vitesse du player")]
    public float MinSpeed = 15f;

    [Header("Valeur accélération/frein")]
    [Tooltip("Taux d'accélération/de freinage du player")]
    public float Taux_Acc_Fre = 0.25f;

    [Header("Coefficient")]
    [Tooltip("Définit le coefficient multiplicateur/diviseur des bonus/malus")]
    public float CoefficientEvent = 1.5f;

    [Header("Chrono")]
    [Tooltip("Définit si le chrono est présent ou non")]
    public bool ChronoBool;
}

[System.Serializable]
public class CChangeColor
{
    [Header("Matériaux")]
    [Tooltip("Matériaux pour chager la couleur de la voituré")]
    public Material PlayerMaterial;

    [Header("Matériaux")]
    [Tooltip("Matériaux pour chager la couleur de la voiture")]
    public Material PlayerMaterialBonus;

    [Header("Matériaux")]
    [Tooltip("Matériaux pour chager la couleur de la voiture")]
    public Material PlayerMaterialMalus;
}


[System.Serializable]
public class CRoad
{
    [Header("Route")]
    [Tooltip("GameObject de la route")]
    public GameObject Road;

    [Header("Route")]
    [Tooltip("GameObject de la route")]
    public GameObject[] PatternRoad;
}
    
[System.Serializable]
public class CObstacle
{
    [Header("Prefab")]
    [Tooltip("Prefab d'obstacle")]
    public GameObject Prefab;

    [Header("Pourcentage")]
    [Tooltip("Pourcentage sur 100")]
    [Range(0f, 100f)] public float Chance = 100f;

    [Header("Chance")]
    [Tooltip("Chance d'apparition")]
    [HideInInspector] public double _weight;
}

[System.Serializable]
public class CChrono
{
    [Tooltip("Temps avant le fin du jeu")]
    public float temps = 60;

    [Tooltip("Temps avant le fin du jeu")]
    public float temps_restant = 60;
}

public class GameManager : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Joueur")]
    [Tooltip("Parametre du joueur")]
    [SerializeField] public CPlayer Player;

    [Header("Chrono")]
    [Tooltip("Parametre du Chrono")]
    [SerializeField] public CChrono Chrono;

    [Header("Obstalce")]
    [Tooltip("Parametre des Obstacles")]
    [SerializeField] public CObstacle[] Obstacle;

    [Header("Road")]
    [Tooltip("Parametre des Roads")]
    [SerializeField] public CRoad Road;

    [Header("Couleur")]
    [Tooltip("Parametre des Couleurs du player")]
    [SerializeField] public CChangeColor Colors;

    [Header("GameManager")]
    [Tooltip("GameManager de la scène GameScène")]
    public static GameManager instance;

    [Header("Level courant")]
    [Tooltip("Nom du level courant")]
    public string _current_level_name;

    [Header("Level précédant")]
    [Tooltip("Nom du level précédant")]
    public string _previous_level_name;

    List<AsyncOperation> operations = new();

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Awake")]
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
            Destroy(this);
    }

    [Tooltip("Start")]
    private void Start()
    {
        SceneManager.GetActiveScene(); // Active le lancement de la scène
        LoadLevel("MainMenu"); 
    }

    public void LoadLevel(string scene_name)
    {
        if (scene_name == _current_level_name)
            return;
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);

        if (ao == null )
        {
            Debug.Log("GameManager] Unable to load scene " + scene_name);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _previous_level_name = _current_level_name;
        _current_level_name = scene_name;

        operations.Add(ao);
    }
    public void UnloadLevel(string scene_name)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(scene_name);

        if ( ao == null )
        {
            Debug.Log("GameManager] Unable to unload scene " + scene_name);
                return;
        }

        ao.completed += OnUnloadOperationComplete;
    }
    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Load Complete");

        if (operations.Contains(ao))
        {
            operations.Remove(ao);
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_current_level_name));
    }
    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload completed");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
