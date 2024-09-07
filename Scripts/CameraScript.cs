using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    //Handles camera movement, zooming, researching and turn changing
    //Made by Nathan Perlman
    private int increase = 3, towercount, bottomleft, bottomright, topleft, topright, left, right;
    public float Speed;
    private Vector3 OriginPoint;
    public GameObject researchUI, mainUI;
    private float diffx = -39.4f, diffy = 210f;
    public static int turn = 0, gold = 20;
    public TMP_Text text, researchtext;
    private List<GameObject> lockeditems = new List<GameObject>();
    public List<GameObject> researchcosts = new List<GameObject>();
    public static Dictionary<string, int> unlocked = new Dictionary<string, int>() { { "ArcherTower", 0 }, { "WizardTower", 0 }, { "Barracks", 0 }, { "Castle", 0 }, { "Fortress", 0 }, { "Knight", 0 }, { "Archer", 0 }, { "Untagged", 0 } };
    Dictionary<string, int> costs = new Dictionary<string, int>();

    void Start()
    {
        for (int i = 0; i < GameObject.Find("Tray").transform.childCount; i++)
        {
            lockeditems.Add(GameObject.Find("Tray").transform.GetChild(i).gameObject);
            GameObject.Find("Tray").transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!researchUI.activeInHierarchy)
        {
            text.text = "<sprite index=0> " + gold.ToString();
            if (Input.GetKey(KeyCode.UpArrow) && Camera.main.orthographicSize > 5 || Input.GetKey(KeyCode.W) && Camera.main.orthographicSize > 5)
            {
                Camera.main.orthographicSize -= 0.2f;
            }
            if (Input.GetKey(KeyCode.DownArrow) && Camera.main.orthographicSize < 14 || Input.GetKey(KeyCode.S) && Camera.main.orthographicSize < 14)
            {
                Camera.main.orthographicSize += 0.2f;
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                mainUI.SetActive(!mainUI.activeSelf);
            }
            if (Input.GetMouseButtonDown(1))
            {
                OriginPoint = Input.mousePosition;
                return;
            }
            if (!Input.GetMouseButton(1))
            {
                return;
            }
            Vector3 position = Camera.main.ScreenToViewportPoint(Input.mousePosition - OriginPoint);
            Vector3 move = new Vector3(position.x * Speed, position.y * Speed, 0);
            transform.Translate(move, Space.World);
        }
        else
        {
            researchtext.text = "<sprite index=0> " + gold.ToString();
            costs = new Dictionary<string, int>() { { "Archer", increase }, { "ArcherTower", increase + 3 }, { "ArcherTower2", increase + 5 }, { "ArcherTower3", increase + 9 }, { "WizardTower", increase + 13 }, { "WizardTower2", increase + 19 }, { "WizardTower3", increase + 27 }, { "House", increase }, { "Blacksmith", increase + 5 }, { "Corn", increase + 12 }, { "Wheat", increase + 6 }, { "Mill", increase + 10 }, { "Barracks", increase + 3 }, { "Barracks2", increase + 6 }, { "Barracks3", increase + 10 }, { "Wall", increase + 2 }, { "Castle2", increase + 13 }, { "Fortress", increase + 12 }, { "Fortress2", increase + 22 }, { "Fortress3", increase + 32 }, { "Castle3", increase + 30 }, { "Archery", increase + 6 }, { "LighterArmor", increase + 3 }, { "BetterArmor", increase + 6 }, { "BetterWalls", increase + 11 }, { "BetterWeapons", increase + 7 }, { "Healing", increase + 6 }, { "IncreasedProduction", increase + 17 }, { "MountainClimbing", increase + 3 }, { "Banking", increase + 14 } };
        }
    }

    public void changeturn()
    {
        turn += 1;
        gold += 5;
        if(turn == 4)
        {
            turn = 0;
        }
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
        if (gold >= costs[EventSystem.current.currentSelectedGameObject.name])
        {
            gold -= costs[EventSystem.current.currentSelectedGameObject.name];
            unlocked[EventSystem.current.currentSelectedGameObject.tag] += 1;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = "<sprite=\"GreenCheck\" index=0>";
            researchcosts.Remove(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject);
            for (int i = 0; i < researchcosts.Count; i++)
            {
                researchcosts[i].GetComponent<TMP_Text>().text = "<sprite index=0> " + (costs[researchcosts[i].transform.parent.name] + 1).ToString();
            }
            increase += 1;
            if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "bottomleft" && bottomleft <= 3)
            {
                bottomleft += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(bottomleft).GetComponent<Button>().interactable = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "bottomright" && bottomright <= 4)
            {
                bottomright += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(bottomright).GetComponent<Button>().interactable = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "topleft" && topleft <= 3)
            {
                topleft += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(topleft).GetComponent<Button>().interactable = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "topright" && topright <= 4)
            {
                topright += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(topright).GetComponent<Button>().interactable = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "right" && right <= 1)
            {
                right += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(right).GetComponent<Button>().interactable = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.transform.parent.name == "left" && left <= 1)
            {
                left += 1;
                EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(left).GetComponent<Button>().interactable = true;
            }
            if (EventSystem.current.currentSelectedGameObject.name == "Barracks")
            {
                GameObject.Find("topleft").transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "House")
            {
                GameObject.Find("bottomleft").transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
            try
            {
                lockeditems.Find(x => x.name == EventSystem.current.currentSelectedGameObject.name).SetActive(true);
                lockeditems.Find(x => x.name == EventSystem.current.currentSelectedGameObject.name).GetComponent<RectTransform>().anchoredPosition = new Vector2(diffx, diffy);
                towercount += 1;
                diffx += 78.6f;
                if (towercount % 2 == 0)
                {
                    diffx = -39.4f;
                    diffy -= 105f;
                }
            }
            catch (Exception)
            {
                //Item researched was not a tower
            }
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public static IEnumerator Shake(GameObject objecttoshake) 
    {
        for ( int i = 0; i < 10; i++)
        {
            objecttoshake.transform.localPosition += new Vector3(5f, 0, 0);
            yield return new WaitForSeconds(0.01f);
            objecttoshake.transform.localPosition -= new Vector3(5f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
     }
}
