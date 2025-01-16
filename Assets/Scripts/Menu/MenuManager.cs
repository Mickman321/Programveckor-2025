using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region variabels

    public GameObject general_menu;
    public GameObject graphics_menu;
    public GameObject audio_menu;
    //
    public GameObject coreMenu;
    public GameObject settingMenu;
    public GameObject menuCamera;
    public GameObject targetObject;
    public GameObject cameraObject;
    private TheMove theMove;
    private NewMove newMove;
    private bool fadeOut;
    public CanvasGroup canvasGroup;
    #endregion
    public void Awake()
    {
         theMove = targetObject.GetComponent<TheMove>();
         newMove = cameraObject.GetComponent<NewMove>();

         theMove.enabled = false;
          newMove.enabled = false;
    }

    void FixedUpdate(){
        if (fadeOut)
        {
            canvasGroup.alpha -= Time.deltaTime;
        }
        
    }
    #region MainMenuButtons
    public void StartGame(){
        menuCamera.SetActive(false);
        //this.gameObject.SetActive(false);
        fadeOut = true;
        StartCoroutine(delay(2f));
    }

    public void Settings(){
        settingMenu.SetActive(true);
        coreMenu.SetActive(false);
    }
    public void SettingsGoBack(){
        settingMenu.SetActive(false);
        coreMenu.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }
    #endregion
    
    public void General(){
        closeAllSettings();
        general_menu.SetActive(true);
    }

    public void Graphics(){
        closeAllSettings();
        graphics_menu.SetActive(true);
    }

    public void Audio(){
        closeAllSettings();
        audio_menu.SetActive(true);
    }

    void closeAllSettings(){
        general_menu.SetActive(false);
        graphics_menu.SetActive(false);
        audio_menu.SetActive(false);
    }
    public void Fullscreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }
    IEnumerator delay(float second){
        yield return new WaitForSeconds(second);
        
        theMove.enabled = true;
        newMove.enabled = true;
        this.gameObject.SetActive(false);
        Debug.Log("lalalalaladelaydleay");
    }
}
