using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    //Param�tre :

    //////////////////////////////////////////////////////////////////////////////////////////////////
    
    [Header("Liste Obstacles")]
    [Tooltip("Liste d'objet de la Classe CObstacle")]
    [SerializeField] private CObstacle[] Obstacle;

    [Header("Liste Obstacles")]
    [Tooltip("Liste d'objet de la Classe CObstacle")]
    [SerializeField] private CRoad Road;

    [Header("Compteur")]
    [Tooltip("Accumulation du taux (peut etre superieure � 100) de probabilit�")]
    private double accumulatedWeights;

    [Header("Al�atoire")]
    [Tooltip("Donne une nouvelle liste al�atoire de chiffre (un refresh)")]
    private System.Random rand = new System.Random();

    [Header("Route_Parent_Arbor�scence")]
    [Tooltip("M�re de toutes les futures routes")]
    public Transform trsfmParent;

    [Header("Point de spawn")]
    [Tooltip("GameObject du SpawnPointRoad, il permet de cr�er la route")]
    public GameObject SpawnPointRoad;

    [Header("Taille")]
    [Tooltip("Longueur de la route")]
    private float Size_Road = 0f;

    [Header("Taille")]
    [Tooltip("Longueur de la route")]
    private float Size_PatternRoad;

    [Header("Distance")]
    [Tooltip("Ajoute une distance constante � SpawnPointRoad")]
    private float UpSpawnPointRoad;

    [Header("Compteur")]
    [Tooltip("Compteur pour cr�er pattern de road")]
    [SerializeField]
    private int Count_pattern = 0;

    [Header("Compteur")]
    [Tooltip("Compteur pour cr�er Objet")]
    [SerializeField]
    private int Count_Prop = 0;

    [Header("Limite")]
    [Tooltip("Toutes les 5 routes cr�er un obstacle")]
    [SerializeField]
    private int Next_Prop = 5;

    [Header("RoadCreate")]
    [Tooltip("Multiplicateur de create road (pour augmenter la distance d'apparition des roads)")]
    [SerializeField]
    private int RoadCreate = 5;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////
    [Tooltip("Start")]
    private void Start()
    {
        Obstacle = GameManager.instance.Obstacle;
        Road = GameManager.instance.Road;

        CalculatedWeights();

        Size_Road = Road.Road.GetComponent<Renderer>().bounds.size.z;
        UpSpawnPointRoad = transform.position.z + Size_Road;
        for (int i = 0; i < 30; i++)
        {
            GenerateRoad();
            Count_pattern++;
        }
    }

    [Tooltip("Update")]
    private void Update()
    {
        Generate();
    }

    private void Generate()
    {
        if (GameManager.instance.Player.player.transform.position.z* RoadCreate > UpSpawnPointRoad && Count_pattern % 40 != 0)
        {
            UpSpawnPointRoad += Size_Road;
            Count_pattern++;
            GenerateRoad();
            if (Count_Prop >= Next_Prop)
            {
                SpawnObstacle();
                Count_Prop = 0;
            }
            else
            {
                Count_Prop++;
            }
        }       
        if (Count_pattern % 40 == 0)
        {
            Size_PatternRoad = 0;
            int random = Random.Range(0, Road.PatternRoad.Length);
            foreach (Transform Child in Road.PatternRoad[random].transform)
            {
                Size_PatternRoad += Child.GetComponent<Renderer>().bounds.size.z;
            }
            UpSpawnPointRoad += Size_PatternRoad;
            GeneratePatternRoad(Road.PatternRoad[random]);
            Count_pattern = 1;
        }
    }

    [Tooltip("G�n�re la route")]
    private void GenerateRoad()
    {
        SpawnPointRoad.transform.Translate(new Vector3(0, 0, Size_Road));
        GameObject road = Instantiate(Road.Road, SpawnPointRoad.transform.position, Quaternion.Euler(0, 90, 0), trsfmParent);
    }
    
    [Tooltip("G�n�re la route")]
    void GeneratePatternRoad(GameObject patternroad)
    {
        SpawnPointRoad.transform.Translate(new Vector3(0, 0, Size_Road));
        GameObject Patternroad = Instantiate(patternroad, SpawnPointRoad.transform.position, Quaternion.identity, trsfmParent);
        SpawnPointRoad.transform.Translate(new Vector3(0, 0, Size_PatternRoad - Size_Road));
    }

    [Tooltip("G�n�re les obstacles le long le long de la route")]
    private void SpawnObstacle ()
    {
        CObstacle RandomPrefab = Obstacle[GetRandomEnemyIndex()];
        GameObject obstacle = Instantiate(RandomPrefab.Prefab, SpawnPointRoad.transform.position + new Vector3(Random.Range(-Size_Road * 2, Size_Road * 2), 2f, 0), Quaternion.identity, trsfmParent);
    }

    [Tooltip("Renvoi l'index de l'objet a cr�er celon les probabilit�s")]
    private int GetRandomEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < Obstacle.Length; i++)
            if (Obstacle[i]._weight >= r)
                return i;

        return 0;
    }

    [Tooltip("Calcul le taux de probabilit� (peut �tre sup�rieur � 100)")]
    private void CalculatedWeights()
    {
        accumulatedWeights = 0f;
        foreach (CObstacle obstacle in Obstacle)
        {
            accumulatedWeights += obstacle.Chance;
            obstacle._weight = accumulatedWeights;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
