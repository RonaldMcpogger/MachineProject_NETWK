using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Host;
    public GameObject Join;
    public GameObject Game;
    public GameObject Loading;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        Host.SetActive(false);
        Join.SetActive(false);
        Game.SetActive(false);
        Loading.SetActive(false);
    }

    public void MainMenuActive()
    {
        MainMenu.SetActive(true);
        Host.SetActive(false);
        Join.SetActive(false);
        Game.SetActive(false);
        Loading.SetActive(false);
    }
    public void HostActive()
    {
        MainMenu.SetActive(false);
        Host.SetActive(true);
        Join.SetActive(false);
        Game.SetActive(false);
        Loading.SetActive(false);
    }
    public void JoinActive()
    {
        MainMenu.SetActive(false);
        Host.SetActive(false);
        Join.SetActive(true);
        Game.SetActive(false);
        Loading.SetActive(false);
    }
    public void GameActive()
    {
        MainMenu.SetActive(false);
        Host.SetActive(false);
        Join.SetActive(false);
        Game.SetActive(true);
        Loading.SetActive(false);
    }
    public void LoadingActive()
    {
        MainMenu.SetActive(false);
        Host.SetActive(false);
        Join.SetActive(false);
        Game.SetActive(true);
        Loading.SetActive(true);
    }


}
