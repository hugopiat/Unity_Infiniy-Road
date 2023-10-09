using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Paramètre :

    //////////////////////////////////////////////////////////////////////////////////////////////////
    
    [Tooltip("Image du Menu")]
    public GameObject Mainmenu;

    [Tooltip("Image des Levels")]
    public GameObject levelToLoad;

    [Tooltip("Les paramétrages")]
    public GameObject Settings;

    [Tooltip("Les paramétrages")]
    public GameObject Settingscolor;

    public CChangeColor[] Colors;

    public GameObject[] ColorPlayer;

    public CRoad Road;

    public GameObject Player;

    //////////////////////////////////////////////////////////////////////////////////////////////////

    //Fonction :

    //////////////////////////////////////////////////////////////////////////////////////////////////

    [Tooltip("Démarre la scène")]
    public void StartGame()
    {
        levelToLoad.SetActive(true);
        Mainmenu.SetActive(false);
    }

    [Tooltip("Ferme la page des levels")]
    public void CloseLevel()
    {
        levelToLoad.SetActive(false);
        Mainmenu.SetActive(true);
    }

    [Tooltip("Lance Le mode chrono")]
    public void ChronoMode()
    {
        GameChronoMode();
        GameManager.instance.UnloadLevel("MainMenu");
        GameManager.instance.LoadLevel("GameScene");
    }
    
    [Tooltip("Lance Le mode normal")]
    public void NormalMode()
    {
        GameNormalMode();
        GameManager.instance.UnloadLevel("MainMenu");
        GameManager.instance.LoadLevel("GameScene");
    }
    
    [Tooltip("Lance Le mode difficile")]
    public void DifficultMode()
    {
        GameDifficultMode();
        GameManager.instance.UnloadLevel("MainMenu");
        GameManager.instance.LoadLevel("GameScene");
    }

    [Tooltip("Démarre les paramètre")]
    public void SettingsButton() 
    { 
        Settings.SetActive(true);
        Mainmenu.SetActive(false);
    }

    [Tooltip("Ferme les paramètre")]
    public void CloseSettings() 
    { 
        Settings.SetActive(false);
        Mainmenu.SetActive(true);
    }

    [Tooltip("Quitte le jeu")] 
    public void QuitGame() 
    { 
        Application.Quit(); 
    }
    private void GameNormalMode()
    {
        GameManager.instance.Player.ChronoBool = false;
        GameManager.instance.Player.Speed = 20f;
        GameManager.instance.Player.MaxSpeed = 30f;
        GameManager.instance.Player.MinSpeed = 10f;
        GameManager.instance.Player.Taux_Acc_Fre = 0.25f;
        GameManager.instance.Obstacle[0].Chance = 75f;
        GameManager.instance.Obstacle[1].Chance = 15f;
        GameManager.instance.Obstacle[2].Chance = 7.5f;
    }
    private void GameDifficultMode()
    {
        GameManager.instance.Player.ChronoBool = false;
        GameManager.instance.Player.Speed = 40f;
        GameManager.instance.Player.MaxSpeed = 50f;
        GameManager.instance.Player.MinSpeed = 30f;
        GameManager.instance.Player.Taux_Acc_Fre = 0.5f;
        GameManager.instance.Obstacle[0].Chance = 80f;
        GameManager.instance.Obstacle[1].Chance = 5f;
        GameManager.instance.Obstacle[2].Chance = 15f;
    }

    private void GameChronoMode()
    {
        GameManager.instance.Player.ChronoBool = true;
        GameManager.instance.Player.Speed = 20f;
        GameManager.instance.Player.MaxSpeed = 30f;
        GameManager.instance.Player.MinSpeed = 10f;
        GameManager.instance.Player.Taux_Acc_Fre = 0.25f;
        GameManager.instance.Obstacle[0].Chance = 75f;
        GameManager.instance.Obstacle[1].Chance = 12.5f;
        GameManager.instance.Obstacle[2].Chance = 12.5f;
    }

    private void GameRoad()
    {
        GameManager.instance.Road = Road;
    }

    [Tooltip("Démarre les paramètre de la couleur du player")]
    public void SettingsColor()
    {
        Settingscolor.SetActive(true);
        Mainmenu.SetActive(false);
    }

    [Tooltip("Ferme les paramètre de la couleur du player")]
    public void CloseSettingsColor()
    {
        Settingscolor.SetActive(false);
        Mainmenu.SetActive(true);
    }

    public void Color1()
    {
        GameManager.instance.Colors.PlayerMaterial = Colors[0].PlayerMaterial;
        GameManager.instance.Colors.PlayerMaterialBonus = Colors[0].PlayerMaterialBonus;
        GameManager.instance.Colors.PlayerMaterialMalus = Colors[0].PlayerMaterialMalus;
        ColorPlayer[0].SetActive(true);
        ColorPlayer[1].SetActive(false);
        ColorPlayer[2].SetActive(false);
        ColorPlayer[3].SetActive(false);
    }

    public void Color2()
    {
        GameManager.instance.Colors.PlayerMaterial = Colors[1].PlayerMaterial;
        GameManager.instance.Colors.PlayerMaterialBonus = Colors[1].PlayerMaterialBonus;
        GameManager.instance.Colors.PlayerMaterialMalus = Colors[1].PlayerMaterialMalus;
        ColorPlayer[0].SetActive(false);
        ColorPlayer[1].SetActive(true);
        ColorPlayer[2].SetActive(false);
        ColorPlayer[3].SetActive(false);
    }
    public void Color3()
    {
        GameManager.instance.Colors.PlayerMaterial = Colors[2].PlayerMaterial;
        GameManager.instance.Colors.PlayerMaterialBonus = Colors[2].PlayerMaterialBonus;
        GameManager.instance.Colors.PlayerMaterialMalus = Colors[2].PlayerMaterialMalus;
        ColorPlayer[0].SetActive(false);
        ColorPlayer[1].SetActive(false);
        ColorPlayer[2].SetActive(true);
        ColorPlayer[3].SetActive(false);
    }
    public void Color4()
    {
        GameManager.instance.Colors.PlayerMaterial = Colors[3].PlayerMaterial;
        GameManager.instance.Colors.PlayerMaterialBonus = Colors[3].PlayerMaterialBonus;
        GameManager.instance.Colors.PlayerMaterialMalus = Colors[3].PlayerMaterialMalus;
        ColorPlayer[0].SetActive(false);
        ColorPlayer[1].SetActive(false);
        ColorPlayer[2].SetActive(false);
        ColorPlayer[3].SetActive(true);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////
}
