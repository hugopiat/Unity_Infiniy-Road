using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class PlayerController : MonoBehaviour
{
    //Param�tre :

    //////////////////////////////////////////////////////////////////////////////////////////////////


    public GameObject player;

    [Header("Vitesse Texte")]
    [Tooltip("Affiche la vitesse du joueur dans le canvas")]
    public Text txt_speed;

    [Header("Distance Texte")]
    [Tooltip("Affiche la distance parcourue du joueur dans le canvas")]
    public Text txt_distance;
    [Tooltip("Position d�part du joueur")]
    private Vector3 position_start;

    [Header("Propri�t�s vitesse joueur")]
    [Tooltip("Vitesse du player")]
    public float Speed;
    [Tooltip("Vitesse pour tourner")]
    public float TurnSpeed;

    [Header("Axes Propri�t�s")]
    [Tooltip("Valeur horizontale de d�placment")]
    private float HorizontalInput;
    [Tooltip("Valeur verticale de d�placment")]
    private float ForwardInput;

    [Header("Vitesse Propri�t�s")]
    [Tooltip("Maximum vitesse du player")]
    public float MaxSpeed;
    [Tooltip("Minimum vitesse du player")]
    public float MinSpeed;

    [Header("Valeur acc�l�ration/frein")]
    [Tooltip("Taux d'acc�l�ration/de freinage du player")]
    public float Taux_Acc_Fre;

    [Header("Animation acc�l�ration")]
    [Tooltip("Animation du taux d'acc�l�ration du player")]
    public AnimationCurve SpeedAcceleration;

    [Header("Animation frein")]
    [Tooltip("Animation du taux de freinage du player")]
    public AnimationCurve SpeedFrein;

    [Header("Invincibilit�")]
    [Tooltip("D�finit si le player est invincible ou non")]
    private bool Invincible = false;

    [Header("Malus")]
    [Tooltip("D�finit si le player est en malus ou non")]
    private bool Malus = false;

    [Header("Coefficient")]
    [Tooltip("D�finit le coefficient multiplicateur/diviseur des bonus/malus")]
    public float CoefficientEvent;

    [Header("Rigidbody")]
    [Tooltip("Rigidbody du player")]
    private Rigidbody Rb;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Start")]
    public void Start()
    {        
        MAJ(); //Appelle la fonction Mise � jour
    }

    [Tooltip("Update")]
    private void Update()
    {
        TXT(); //Appelle la fonction affichage de texte dans le canvas
        Deplacement(); //Appelle la fonction Mise � jour des d�placement (gr�ce au fl�ches directionnelles)
        Player_Fall(); //Appelle la fonction pour savoir si le player est tomb� de la route
    }

    [Tooltip("FixeUpdate")]
    private void FixedUpdate()
    {
        Avancer(); //Appelle la fonction pour faire avancer le Player
        LimitSpeed(); //Appelle la fonction limite de vitesse (autant pour le maximum que pour le minimum)
        Constant_Accerleration(); //Appelle la fonction D'acc�l�ration constante de la vitesse
    }

    [Tooltip("FixeUpdate")]
    private void Player_Fall()
    {
        if (transform.position.y +2 < position_start.y) //Test si le player est tomb� de la route
        {
            GameManager.instance.UnloadLevel(GameManager.instance._current_level_name);
            GameManager.instance.LoadLevel("Looser");
        }
    }

    [Tooltip("D�placement")]
    public void Deplacement()
    {
        HorizontalInput = Input.GetAxis("Horizontal"); // R�cup�re l'axe Horizontale
        ForwardInput = Input.GetAxis("Vertical"); // R�cup�re l'axe Verticale
    }

    [Tooltip("Texte dans le Canvas")]
    private void TXT()
    {
        txt_speed.text = ("Speed : " + Mathf.RoundToInt(Speed)); //Met a jour le texte Speed dans le Canvas
        GameManager.instance.Player.Distance = (Mathf.RoundToInt(transform.position.z - position_start.z));// Instancie la distance du player dans le GameManager
        txt_distance.text = ("Distance parcouru : " + Mathf.RoundToInt(GameManager.instance.Player.Distance) + " m"); //Met a jour le texte Distance dans le Canvas
    }

    [Tooltip("D�finit les contr�les de la voiture (Avancer)")]
    public void Avancer()
    {
        float degrees = 30;
        float radians = degrees * Mathf.Deg2Rad;

        if (transform.rotation.y > -radians && transform.rotation.y < radians && !Malus)
        {
            Rb.AddRelativeForce(Vector3.forward * Speed, ForceMode.Acceleration);
            Rb.AddRelativeForce(Vector3.right * Speed * HorizontalInput, ForceMode.Acceleration);
            transform.Rotate(Vector3.up * TurnSpeed * HorizontalInput);
        }
        else if (transform.rotation.y > -radians && transform.rotation.y < radians && Malus)
        {
            Rb.AddRelativeForce(Vector3.forward * Speed, ForceMode.Acceleration);
            Rb.AddRelativeForce(Vector3.right * Speed * (-HorizontalInput), ForceMode.Acceleration);
            transform.Rotate(Vector3.up * TurnSpeed * (-HorizontalInput));
        }
        else
        {
            Rb.AddRelativeForce(Vector3.forward * 0, ForceMode.Acceleration);
            Rb.AddRelativeForce(Vector3.right * 0, ForceMode.Acceleration);
            transform.Rotate(Vector3.up * TurnSpeed * HorizontalInput);
        }
    }

    [Tooltip("D�finit la vitesse (Croissante ou D�croissante (avec des limites de vitesse : min et max))")]
    public void LimitSpeed() // Mise � jour de la vitesse celon l'axe vertical
    {
        if (ForwardInput > 0 && !Invincible)
        {
            if (Speed < MaxSpeed)
                Speed += Taux_Acc_Fre * SpeedAcceleration.Evaluate(ForwardInput);
            else
                Speed = MaxSpeed;
        }
        else if (ForwardInput < 0 && !Invincible)
        {
            if (Speed > MinSpeed)
                Speed -= Taux_Acc_Fre * SpeedFrein.Evaluate(ForwardInput);
            else
                Speed = MinSpeed;
        }
        else if (ForwardInput == 0 && !Malus && !Invincible)
        {
            if (Speed > MinSpeed)
                Speed -= 0.2f;

            else if (Speed < MinSpeed)
                Speed = MinSpeed;
        }
    }

    [Tooltip("Acc�l�ration constante des limites")]
    public void Constant_Accerleration() // Mise � jour de la vitesse selon le temps qui passe
    {
        MinSpeed += Taux_Acc_Fre/Speed; //Augmente progressivement le MinSpeed
        MaxSpeed += Taux_Acc_Fre/Speed; //Augmente progressivement le MaxSpeed
    }

    [Tooltip("Active l'invincibilit�")]
    public void Invincibility() {
        Invincible = true; //Met l'Invinciblit�(Booleen) � True
        Speed = Speed * CoefficientEvent; //Multiplie la vitesse par le coefficient de l'evenement bonus
        TurnSpeed = TurnSpeed / CoefficientEvent; //Divise la vitesse pour tourner par le coefficient de l'evenement bonus
        StartCoroutine(InvincibilityDelay()); // Active les secondes � attendre
    } 

    [Tooltip("Compteur de 5 sec pour le bonus (invincibilit�)")]
    public IEnumerator InvincibilityDelay()    {
        yield return new WaitForSeconds(3.0f); //Attends 3 secondes
        Speed = Speed / CoefficientEvent; //Divise la vitesse par le coefficient de l'evenement bonus
        TurnSpeed = TurnSpeed * CoefficientEvent; //Multiplie la vitesse pour tourner par le coefficient de l'evenement bonus
        yield return new WaitForSeconds(2.0f); //Attends 2 secondes
        Invincible = false; //Met l'Invinciblit�(Booleen) � False
    }
    
    
    [Tooltip("Active le Malus")]
    public void MalusFunction() {
        Malus = true; //Met Malus(Booleen) � True
        StartCoroutine(MalusDelay()); // Active les secondes � attendre
    }

    [Tooltip("Compteur de 5 sec pour le malus")]
    public IEnumerator MalusDelay() {
        yield return new WaitForSeconds(5.0f); //Attends 5 secondes
        Malus = false; //Met Malus(Booleen) � False
    }

    [Tooltip("Setteur et Getteur de speed")]
    public void SetSpeed(float speed) { Speed = speed; }//Donne une nouvelle valeur � Speed
    public float GetSpeed() { return Speed; }// Retourne le Speed du player

    [Tooltip("Setteur et Getteur de MaxSpeed")]
    public void SetMaxSpeed(float Maxspeed) { MaxSpeed = Maxspeed; }//Donne une nouvelle valeur � MaxSpeed
    public float GetMaxSpeed() { return MaxSpeed; }// Retourne le MaxSpeed du player

    [Tooltip("Setteur et Getteur de MaxSpeed")]
    public void SetMinSpeed(float Minspeed) { MinSpeed = Minspeed; }//Donne une nouvelle valeur � MinSpeed
    public float GetMinSpeed() { return MinSpeed; }// Retourne le MinSpeed du player

    [Tooltip("Setteur et Getteur de Invincible")]
    public void SetInvincibility(bool invincible) { Invincible = invincible; }//Donne une nouvelle valeur � Invinciblit�
    public bool GetInvincibility() { return Invincible; }// Retourne l'Invinciblit� du player

    [Tooltip("Setteur et Getteur de Malus")]
    public void SetMalus(bool malus) { Malus = malus; }//Donne une nouvelle valeur � Malus
    public bool GetMalus() { return Malus; }// Retourne le Malus du player

    [Tooltip("Setteur et Getteur de TurnSpeed")]
    public void SetTurnSpeed(float turnspeed) { TurnSpeed = turnspeed; } //Donne une nouvelle valeur � TurnSpeed
    public float GetTurnSpeed() { return TurnSpeed; } // Retourne le TurnSpeed du player

    [Tooltip("Met � jour pour utiliser les param�tres de GameManager")]
    public void MAJ()
    {
        player.GetComponent<Chrono>().enabled = GameManager.instance.Player.ChronoBool;
        Rb = GetComponent<Rigidbody>(); // R�cup�re le Component Rigidbody du player
        position_start = transform.position; // D�finit la position de d�part du player
        GameManager.instance.Player.player = this.gameObject; // Instancie le player dans le GameManager 
        CoefficientEvent = GameManager.instance.Player.CoefficientEvent; // Recup�re le CoefficientEvent du GameManager
        Taux_Acc_Fre = GameManager.instance.Player.Taux_Acc_Fre;// Recup�re le Taux_Acc_Fre du GameManager
        Speed = GameManager.instance.Player.Speed;// Recup�re le Speed du GameManager
        TurnSpeed = GameManager.instance.Player.TurnSpeed;// Recup�re le TurnSpeed du GameManager
        MaxSpeed = GameManager.instance.Player.MaxSpeed;// Recup�re le MaxSpeed du GameManager
        MinSpeed = GameManager.instance.Player.MinSpeed;// Recup�re le MinSpeed du GameManager
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
