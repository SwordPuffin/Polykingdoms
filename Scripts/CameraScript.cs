using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    //Handles camera movement, zooming, researching and turn changing
    //Made by Nathan Perlman
    private int increase = 3, towercount, bottomleft, bottomright, topleft, topright;
    public float Speed;
    private Vector3 OriginPoint;
    public GameObject researchUI, mainUI;
    public static float diffx = -42.8f, diffy = 114f;
    public static int turn = 0, gold = 20;
    public TMP_Text text;
    private List<GameObject> lockeditems = new List<GameObject>();
    public static Dictionary<string, int> unlocked = new Dictionary<string, int>(){{"ArcherTower", 0}, {"WizardTower", 0}, {"Barracks", 0}, {"Castle", 0}, {"Fortress", 0}, {"Untagged", 0}}; 
    private Dictionary<string, int> costs;
    void Start()
    {
        costs = new Dictionary<string, int>(){{"Archer", increase}, {"ArcherTower", increase + 3}, {"ArcherTower2", increase + 5}, {"ArcherTower3", increase + 9}, {"WizardTower", increase + 13}, {"WizardTower2", increase  + 20}, {"WizardTower3", increase + 24}, {"House", increase}, {"Blacksmith", increase + 4}, {"Corn", increase + 7}, {"Wheat", increase + 10}, {"Mill", increase + 18}, {"Barracks", increase + 3}, {"Barracks2", increase + 6}, {"Barracks3", increase + 10}, {"Wall", increase + 2}, {"Castle2", increase + 13}, {"Fortress", increase + 15}, {"Fortress2", increase + 20}, {"Fortress3", increase + 25}, {"Castle3", increase + 30}, {"Fortify", increase + 6}, {"LighterArmor", increase + 3}, {"BetterArmor", increase + 6}, {"BetterWalls", increase + 8}, {"BetterWeapons", increase + 3}, {"Healing", increase + 1}, {"IncreasedProduction", increase + 4}, {"MountainClimbing", increase + 3}, {"Banking", increase + 8}}; 
        for (int i = 0; i < GameObject.Find("Tray").transform.childCount; i++)
        {
            lockeditems.Add(GameObject.Find("Tray").transform.GetChild(i).gameObject);
            GameObject.Find("Tray").transform.GetChild(i).gameObject.SetActive(false);
        }    
    }
    
    void Update()
    {
        if(!researchUI.activeInHierarchy)
        {
            text.text = gold.ToString();
            if(Input.GetKey(KeyCode.UpArrow) && Camera.main.orthographicSize > 1 || Input.GetKey(KeyCode.W) && Camera.main.orthographicSize > 1)
            {
                Camera.main.orthographicSize -= 0.2f;
            }
            if(Input.GetKey(KeyCode.DownArrow) && Camera.main.orthographicSize < 14 || Input.GetKey(KeyCode.S) && Camera.main.orthographicSize < 14)
            {
                Camera.main.orthographicSize += 0.2f;
            }
            if(Input.GetMouseButtonDown(1))
            {
                OriginPoint = Input.mousePosition;
                return;
            }
            if(!Input.GetMouseButton(1))
            {
                return;
            }
            Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition - OriginPoint);
            Vector3 move = new Vector3(position.x * Speed, position.y * Speed, 0);
            transform.Translate(move, Space.World);
        }
    }

    public void changeturn()
    {
        turn += 1;
        gold += 5;
    }
    public void toresearch()
    {
        researchUI.SetActive(true);
        mainUI.SetActive(false);
    }
    public void tomain()
    {
        mainUI.SetActive(true);
        researchUI.SetActive(false);
    }
    public void researchbutton()
    {
        if(gold >= costs[EventSystem.current.currentSelectedGameObject.name])
        {
            if(EventSystem.current.currentSelectedGameObject.transform.parent.name == "bottomleft" && bottomleft <= 3)
            {
                bottomleft += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(bottomleft).GetComponent<Button>().interactable = true;
            }
            else if(EventSystem.current.currentSelectedGameObject.transform.parent.name == "bottomright" && bottomright <= 4)
            {
                bottomright += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(bottomright).GetComponent<Button>().interactable = true;
            }
            else if(EventSystem.current.currentSelectedGameObject.transform.parent.name == "topleft" && topleft <= 3)
            {
                topleft += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(topleft).GetComponent<Button>().interactable = true;
            }
            else if(EventSystem.current.currentSelectedGameObject.transform.parent.name == "topright" && topright <= 4)
            {
                topright += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(topright).GetComponent<Button>().interactable = true;
            }
            if(EventSystem.current.currentSelectedGameObject.name == "Barracks")
            {
                GameObject.Find("topleft").transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
            if(EventSystem.current.currentSelectedGameObject.name == "House")
            {
                GameObject.Find("bottomleft").transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
            try
            {
                lockeditems.Find(x => x.name == EventSystem.current.currentSelectedGameObject.name).SetActive(true);
                lockeditems.Find(x => x.name == EventSystem.current.currentSelectedGameObject.name).GetComponent<RectTransform>().anchoredPosition = new Vector2(diffx, diffy);
                towercount += 1;
                diffx += 74.4f;
            } 
            catch(Exception)
            {
                //Item researched was not a tower
            }
            gold -= costs[EventSystem.current.currentSelectedGameObject.name];
            CameraScript.unlocked[EventSystem.current.currentSelectedGameObject.tag] += 1;
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            increase += 1;
            if(towercount % 2 == 0)
            {
                diffx = -42.8f;
                diffy -= 67.6f;
            }
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }
}
