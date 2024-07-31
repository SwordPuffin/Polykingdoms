using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceScript : MonoBehaviour
{
    //Script that handles placement of buldings
    //Made by Nathan Perlman 
    public static GameObject activeobject, location;
    Dictionary<string, float> adjustments = new Dictionary<string, float>(){{"House", 0.6f}, {"ArcherTower", 0.75f}, {"Blacksmith", 0.55f}, {"Barracks", 0.75f}, {"Corn", -0.245f}, {"Wheat", -0.2f}, {"Mill", 1f}, {"Fortress", 1f}, {"Wall", 0.55f}, {"WizardTower", 0.75f}}; 
    Dictionary<string, int> costs = new Dictionary<string, int>(){{"House", 1}, {"ArcherTower", 6}, {"Blacksmith", 4}, {"Barracks", 8}, {"Corn", 2}, {"Wheat", 2}, {"Mill", 5}, {"Fortress", 12}, {"Wall", 4}, {"WizardTower", 6}}; 
    void OnMouseOver()
    {
        activeobject = TrayScript.activeobject;
        location = gameObject;
        if(activeobject != null)
        {
            activeobject.layer = 2;
            if(gameObject.transform.childCount > 0)
            {
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
            if(activeobject.name == "Mill" || activeobject.name == "Fortress" || activeobject.name == "Wall")
            {
                activeobject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = activeobject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            activeobject.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;    
            activeobject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + adjustments[activeobject.name], -1);
            if(Input.GetKey(KeyCode.Q))
            {
                CameraScript.gold += costs[gameObject.name];
                activeobject.transform.position = new Vector3(-53, 23, 0);  
                activeobject = null;
            }
        }
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true; 
        if(gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void OnMouseDown()
    {
        try
        {
            activeobject = TrayScript.activeobject;
            activeobject.layer = 0;
            if(gameObject.transform.childCount == 1 && activeobject != null)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
            }
            if(activeobject.name == "Wheat" && activeobject != null || activeobject.name == "Corn" && activeobject != null)
            {
                Destroy(gameObject);
            }
            if(CameraScript.gold >= costs[activeobject.name])
            {
                CameraScript.gold -= costs[activeobject.name];
                Instantiate(activeobject, new Vector3(transform.position.x, transform.position.y + adjustments[activeobject.name], -1), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder; 
            }
            else
            {
                Debug.Log("Not enough gold");
            }
            activeobject.transform.position = new Vector3(-53, 23, 0);  
            TrayScript.activeobject = null;
        } 
        catch(Exception)
        {
            //Error caused by lack of a activeobject being null when it is being used. Pointless error, do nothing
        }
    }
}
