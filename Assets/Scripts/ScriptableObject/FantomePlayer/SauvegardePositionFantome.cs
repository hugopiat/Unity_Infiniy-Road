using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauvegardePositionFantome : MonoBehaviour
{
    [SerializeField] public InfoChronoJoueur ChronoJoueur;
    [SerializeField] private float dureeChrono;

    [Header("GameManager")]
    [Tooltip("GameManager de la scène GameScène")]
    public static SauvegardePositionFantome instance;

    [Tooltip("Awake")]
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
            Destroy(this);
    }

    public void Start()
    {
        ChronoJoueur = new InfoChronoJoueur();
        ChronoJoueur.JourHeureChronoStr = Time.time.ToString();
        dureeChrono = 0;
        InvokeRepeating("AjouterPosition", 0f, 0.01f);
        InvokeRepeating("AjouterRotation", 0f, 0.01f);
    }
    public void Update()
    {
        dureeChrono += Time.deltaTime;
        ChronoJoueur.tempsChrono = GameManager.instance.Chrono.temps;
    }

    private void AjouterPosition()
    {
        ChronoJoueur.positions.Add(new PositionChrono(dureeChrono, transform.position));
    }

    private void AjouterRotation()
    {
        ChronoJoueur.rotation.Add(new RotationChrono(dureeChrono, transform.rotation.eulerAngles));
    }
}
