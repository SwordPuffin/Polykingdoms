using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingScript : MonoBehaviour
{
    Dictionary<string, int> upgrades = new Dictionary<string, int>(){{"Barracks(Clone)", 7}, {"ArcherTower(Clone)", 8}, {"WizardTower(Clone)", 12}, {"Fortress(Clone)", 12}, {"Castle(Clone)", 20}}; 
    public List<Sprite> sprites;
    public GameObject actiontray;
    public static float ylocation;
    public static GameObject activetower;
    private float counter = -1, positionchange = 0;
    private bool unitonbuilding;
    private RaycastHit2D hit;
    
    void OnMouseDown()
    {
        actiontray.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        actiontray.transform.position = new Vector3(transform.position.x - 2.1f, transform.position.y, 0);
        if(gameObject.name != "Barracks(Clone)")
        {
            actiontray.transform.GetChild(0).gameObject.SetActive(true); 
            actiontray.transform.GetChild(0).gameObject.transform.position = new Vector3(actiontray.transform.position.x, gameObject.transform.position.y, 0);
            for (int x = 1; x < 5; x++)
            {
                actiontray.transform.GetChild(x).gameObject.SetActive(false); 
            }
        }
        else
        {
            if(!unitonbuilding)
            {
                ylocation = gameObject.transform.position.y;
                for (int i = 0; i <= (gameObject.GetComponent<SpriteRenderer>().sprite.name[14] - '0'); i++)
                {
                    actiontray.transform.GetChild(i).gameObject.SetActive(true);
                    actiontray.transform.GetChild(i).gameObject.transform.position = new Vector3(actiontray.transform.position.x, actiontray.transform.position.y + positionchange, 0);
                    positionchange -= 1.5f;
                    if(CameraScript.unlocked["Archer"] == 1 && i == (gameObject.GetComponent<SpriteRenderer>().sprite.name[14] - '0'))
                    {
                        actiontray.transform.GetChild(4).gameObject.SetActive(true);
                        actiontray.transform.GetChild(4).gameObject.transform.position = new Vector3(actiontray.transform.position.x, actiontray.transform.position.y + positionchange, 0);
                        actiontray.transform.position = new Vector3(transform.position.x - 2.1f, actiontray.transform.position.y + 0.55F, 0);
                        positionchange -= 1.5f;
                        actiontray.transform.position = new Vector3(transform.position.x - 2.1f, actiontray.transform.position.y + 0.55F, 0);
                    }
                    actiontray.transform.position = new Vector3(transform.position.x - 2.1f, actiontray.transform.position.y + 0.55F, 0);
                }   
            }
            else
            {
                for (int x = 1; x < 5; x++)
                {
                    actiontray.transform.GetChild(x).gameObject.SetActive(false); 
                }
            }
            positionchange = 0;
        }
        if(CameraScript.gold < upgrades[gameObject.name] || CameraScript.unlocked[gameObject.name.Replace("(Clone)", "")] <= counter + 1)
        {
            actiontray.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
        else
        {
            actiontray.transform.GetChild(0).GetComponent<Button>().interactable = true;
            actiontray.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(confirm);
        }
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && gameObject.name.Contains("Tower"))
        {
            GameObject.Find("Arrow").transform.position = gameObject.transform.GetChild(1).transform.position;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>().isTrigger = !hit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>().isTrigger;
            if(gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>().isTrigger)
            {
                activetower = gameObject;
            }
            else 
            {
                activetower = null;
            }
        }
    }

    public void confirm()
    {
        counter += 1f;
        CameraScript.gold -= upgrades[gameObject.name];
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[(int)counter]; 
        actiontray.transform.position = new Vector3(-53, 23, 0);  
    }
    void Update()
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
        if(Input.GetMouseButtonDown(0) && gameObject.name.Contains("(Clone)") && hit.collider.tag != "DoNotHideActionTray" || Input.GetMouseButtonDown(1) && hit.collider.name.Contains("Tower"))
        {
            actiontray.transform.position = new Vector3(-53, 23, 0);  
        }
        if(Input.GetMouseButtonDown(0) && gameObject == activetower && hit.collider.gameObject != activetower && hit.collider.gameObject.GetComponent<SpriteRenderer>().color != Color.red)
        {
            activetower.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = false;
            activetower = null;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Contains("Knight") && gameObject.name.Contains("Tower") || other.gameObject.name == "Archer(Clone)" && gameObject.name.Contains("Tower"))
        {
            unitonbuilding = true;          
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Knight1(Clone)" || other.gameObject.name == "Knight2(Clone)" || other.gameObject.name == "Knight3(Clone)" || other.gameObject.name == "Archer(Clone)")
        {
            unitonbuilding = false;
        }
    }
} 
