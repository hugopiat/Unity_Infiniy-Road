using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaitReload : MonoBehaviour
{

    [Header("Distance Texte")]
    [Tooltip("Affiche la distance parcourue du joueur dans le canvas")]
    [SerializeField] public Text txt_distance;
    [Tooltip("Sauvegarde les données")]
    [SerializeField] private SauvegardeChrono sauvegardeChrono = new SauvegardeChrono();
    [Tooltip("Gameobject de Player")]
    [SerializeField] public GameObject Player;

    private void Start()
    {
        txt_distance.text = "Vous avez parcouru : " + GameManager.instance.Player.Distance + " m\n\nVous allez pouvoir rejouer dans 5 secondes... \nBonne chance !";
        if(Player.GetComponent<Chrono>().enabled)
            sauvegardeChrono.SauvegarderChrono(SauvegardePositionFantome.instance.ChronoJoueur);
        StartCoroutine(WaitForLoad());
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(5);
        GameManager.instance.UnloadLevel("Looser");
        GameManager.instance.LoadLevel("GameScene");

    }
}
