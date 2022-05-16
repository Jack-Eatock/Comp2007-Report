using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public Animator OptionsAnim1;
    public Animator OptionsAnim2;
    public GameObject pauseMenu1;
    public GameObject pauseMenu2;
    public CustomPlayerMovement player;
    public GameObject mainmenu;


   public void OnExit(){
       Application.Quit();
   }

    public void OnStart()
    {
       
    }

    private void Start()
    {
        //Debug.LogError("A");
    }

    public void OnOptionsClose()
    {
       
        Debug.Log("PAUSEas");

        OptionsAnim1.SetTrigger("Close");
        OptionsAnim2.SetTrigger("Close");
        StartCoroutine(ClosingPause());

    }



    private IEnumerator ClosingPause()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        player.paused = false;
        Time.timeScale = 1;

        if (mainmenu.activeSelf)
        {
            pauseMenu2.SetActive(false);
        }
        else
        {
            pauseMenu1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public void OnOptionsOpen()
    {
        Debug.Log("PAUSE");
        player.paused = true;
      
        if (mainmenu.activeSelf)
        {
            pauseMenu2.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu1.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainmenu.activeSelf){ return; }
            if (pauseMenu1.activeSelf)
            {
                OnOptionsClose();
            }
            else
            {
                OnOptionsOpen();
            }
        
        }
    }


}
