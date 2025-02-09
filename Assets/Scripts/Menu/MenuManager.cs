using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject targetnlc;

    public GameObject general_menu;
    public GameObject graphics_menu;
    public GameObject audio_menu;
    public GameObject coreMenu;
    public GameObject settingMenu;
    public GameObject menuCamera;
    public GameObject targetObject;
    public GameObject cameraObject;
    private TheMove theMove;
    private NewMove newMove;
    private bool fadeOut;
    public CanvasGroup canvasGroup;
    public void Awake()
    {
        targetnlc.SetActive(false); 
        Cursor.lockState = CursorLockMode.None; // Lock the cursor
        Cursor.visible = true;
         theMove = targetObject.GetComponent<TheMove>();
         newMove = cameraObject.GetComponent<NewMove>();
        Debug.Log("awake?");
    }

    void FixedUpdate(){
        if (fadeOut)
        {
            canvasGroup.alpha -= Time.deltaTime;
        }
        
    }
    public void StartGame(){
        
        menuCamera.SetActive(false);
        //this.gameObject.SetActive(false);
        fadeOut = true;
        StartCoroutine(delay(2f));
        new WaitForSeconds(2);
        targetnlc.SetActive(true); Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false;
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
