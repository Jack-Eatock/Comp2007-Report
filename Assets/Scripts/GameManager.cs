using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GamePlay;

    public void StartGame()
    {
        StartCoroutine(Starting());
    }

    public IEnumerator Starting()
    {
        MainMenu.SetActive(false);
        GamePlay.SetActive(true);
        yield return null;
    }

    public void ReturnToMenu()
    {
        MainMenu.SetActive(true);
        GamePlay.SetActive(false);
    }

}
