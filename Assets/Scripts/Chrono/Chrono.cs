using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Temps avant le fin du jeu")]
    public float temps;

    [Tooltip("Timer restant dans le canvas")]
    public Text timertext;

    [Tooltip("String de la scène Loose")]
    private string SceneLoose = "Looser";

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        temps = GameManager.instance.Chrono.temps;
    }

    void Update()
    {
        Actualisation();
    }

    public void Actualisation()
    {
        timertext.text = (Mathf.RoundToInt(temps) + " secondes");
        if (temps >= 0)
        {
            temps -= Time.deltaTime;
            GameManager.instance.Chrono.temps_restant = temps;
        }
        if (temps <= 0)
        {
            GameManager.instance.UnloadLevel(GameManager.instance._current_level_name);
            GameManager.instance.LoadLevel(SceneLoose);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
