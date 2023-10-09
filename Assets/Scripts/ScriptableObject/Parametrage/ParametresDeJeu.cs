using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CreateAssetMenu(fileName = "param�tres_UnNiveauDeJeu",
//                 menuName = "ScriptableObjects/ParametresDeJeu ", order = 1)]

public class ParametresDeJeu : ScriptableObject
{
    [System.Serializable]

    [Tooltip("Une classe pour les objets avec leurs probabilit� d'apparition")]
    public class UnObstacle
    {
        public GameObject obstacle;
        public float probaTirage;
    }

    [Tooltip("Vitesse Minimum du joueur")]
    public float vMin;

    [Tooltip("Vitesse Maximum du joueur")]
    public float vMax;

    [Tooltip("Acc�l�ration du joueur")]
    public AnimationCurve acc;

    [Tooltip("Fr�quence des objets")]
    public float freqObst;

    [Tooltip("Liste d'objets de la classe 'UnObstacle' ")]
    public List<UnObstacle> lesObstacles;

    [Tooltip("Initialisation des variables des param�tres de jeu")]
    public void Init(float p_vMin, float p_vMax, float p_freqObst)
    {
        vMin = p_vMin;
        vMax = p_vMax;
        acc = null;
        freqObst = p_freqObst;
        lesObstacles = null;
    }

    //[Tooltip("Retourne un nouveau Scriptable avec les nouveaux param�tres de jeu (enregistr�) ")]
    //public static ParametresDeJeu Create_RT_ParametresDeJeu(float p_vMin, float p_vMax, float p_freqObst)
    //{
    //    ParametresDeJeu pj;
    //    pj = ScriptableObject.CreateInstance<ParametresDeJeu>();
    //    pj.name = "param�tres_UnNiveauDeJeu_RT";
    //    pj.Init(p_vMin, p_vMax, p_freqObst);
    //    string name = "RTcreated_" + typeof(ParametresDeJeu).Name;
    //    name += AssetDatabase.FindAssets(name, null).Length + 1;
    //    AssetDatabase.CreateAsset(pj, "Assets/" + name + ".asset");
    //    AssetDatabase.SaveAssets();
    //    return pj;
    //}
}
