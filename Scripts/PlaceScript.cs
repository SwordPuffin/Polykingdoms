using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceScript : MonoBehaviour
{
    //Script that handles placement of buldings
    //Made by Nathan Perlman 
    public static GameObject location;
    private GameObject activeobject;
    Dictionary<string, float> adjustments = new Dictionary<string, float>(){{"House", 0.4f}, {"ArcherTower", 0.55f}, {"Blacksmith", 0.35f}, {"Barracks", 0.3f}, {"Corn", -0.52f}, {"Wheat", -0.52f}, {"Mill", 0.8f}, {"Fortress", 0.8f}, {"Wall", 0.1f}, {"WizardTower", 0.55f}}; 
    public bool unitontile;
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
            if(activeobject.name == "Mill")
            {
                activeobject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = activeobject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            else if(activeobject.name == "ArcherTower")
            {
                activeobject.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = activeobject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            activeobject.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;    
            activeobject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + adjustments[activeobject.name], -1);
            // if(Input.GetKey(KeyCode.Q))
            // {
            //     CameraScript.gold += costs[gameObject.name];
            //     activeobject.transform.position = new Vector3(-53, 23, 0);  
            //     activeobject = null;
            // }
        }
    }

    void OnMouseExit()
    {
        if(gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnMouseDown()
    {
        try
        {
            if(!gameObject.name.Contains("Water"))
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
                
                CameraScript.gold -= TrayScript.costs[activeobject.name];
                Instantiate(activeobject, new Vector3(transform.position.x, transform.position.y + adjustments[activeobject.name], -1), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder; 
                activeobject.transform.position = new Vector3(-53, 23, 0);  
                TrayScript.activeobject = null;
            }
        } 
        catch(Exception)
        {
            //Error caused by activeobject being null when it is used. Just makes sure it doesn't instantiate something when there is no activeobject
        }
    }
}
