using Steamworks;
using UnityEngine;

public class MainMenuUi : MonoBehaviour
{
    public GameObject[] mainMenu;
    public GameObject[] menuPlay;
    public GameObject menuOption;
    public GameObject menuCredit;

    public void OnClickRetunToMainMenu()
    {
        for (int i = 0; i < mainMenu.Length; i++)
        {
            mainMenu[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < menuPlay.Length; i++)
        {
            menuPlay[i].gameObject.SetActive(false);
        }

        menuOption.SetActive(false);
        menuCredit.SetActive(false);
    }

    public void OnClickMenuPlay()
    {
        for (int i = 0; i < mainMenu.Length; i++)
        {
            mainMenu[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < menuPlay.Length; i++)
        {
            menuPlay[i].gameObject.SetActive(true);
        }

        menuOption.SetActive(false);
        menuCredit.SetActive(false);
    }

    public void OnClickMenuOption()
    {
        for (int i = 0; i < mainMenu.Length; i++)
        {
            mainMenu[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < menuPlay.Length; i++)
        {
            menuPlay[i].gameObject.SetActive(false);
        }

        menuOption.SetActive(true);
        menuCredit.SetActive(false);
    }

    public void OnClickMenuCredit()
    {
        for (int i = 0; i < mainMenu.Length; i++)
        {
            mainMenu[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < menuPlay.Length; i++)
        {
            menuPlay[i].gameObject.SetActive(false);
        }

        menuOption.SetActive(false);
        menuCredit.SetActive(true);
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }

    public void OpenFriendList()
    {
        // Ouvre la liste d'amis Steam
        SteamFriends.ActivateGameOverlay("Friends");
    }
}
