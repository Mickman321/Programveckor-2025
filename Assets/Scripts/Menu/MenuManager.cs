using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCamera;
    public GameObject targetObject;
    public GameObject cameraObject;
    private TheMove theMove;
    private NewMove newMove;
    private bool fadeOut;
    public CanvasGroup canvasGroup;
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
    public void StartGame(){
        menuCamera.SetActive(false);
        //this.gameObject.SetActive(false);
        fadeOut = true;
        StartCoroutine(delay(2f));
    }

    public void Settings(){

    }

    public void Quit(){

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
