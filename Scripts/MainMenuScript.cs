using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class MainMenuScript : MonoBehaviour
{
    public GameObject infopanel, enterworldsize, numberofbots;
    public TMP_InputField worldsize;
    private bool movecredittocentre, movenumberofbotstocentre, moveenterworldsize;
    public static int size;
    public static Dictionary<int, string> colours = new Dictionary<int, string>();
    void Start()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        infopanel.SetActive(movecredittocentre);
        numberofbots.SetActive(movenumberofbotstocentre);
        enterworldsize.SetActive(moveenterworldsize);
    }
    public void credit()
    {
        movecredittocentre = !movecredittocentre;
        movenumberofbotstocentre = false;
        moveenterworldsize = false;
    }
    public void singleplayer()
    {
        try
        {
            if(EventSystem.current.currentSelectedGameObject.name == "Play")
            {
                movenumberofbotstocentre = !movenumberofbotstocentre;
                moveenterworldsize = !moveenterworldsize;
                movecredittocentre = false;
            }
            else if(EventSystem.current.currentSelectedGameObject.name == "Start" && int.Parse(worldsize.text) > 9)
            {
                size = int.Parse(worldsize.text);
                SceneManager.LoadScene("MainGame");
            }
            else
            {
                CameraScript.Shake(EventSystem.current.currentSelectedGameObject);
            }
        } 
        catch(Exception)
        {
            CameraScript.Shake(EventSystem.current.currentSelectedGameObject);
        }
    }
    IEnumerator Move() 
    {
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
                gameObject.transform.localPosition -= new Vector3(5, 0, 0) * Time.deltaTime;
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 20; i++)
            {
                gameObject.transform.localPosition += new Vector3(5, 0, 0) * Time.deltaTime;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
