using System.Collections.Generic;
using UnityEngine;

public class TrayScript : MonoBehaviour
{
    //Scipt that is used when the buttons in the panel or actiontray are clicked
    //Made by Nathan Perlman
    public GameObject building, trainedunit;
    public static Dictionary<string, int> costs = new Dictionary<string, int>(){{"House", 1}, {"ArcherTower", 6}, {"Blacksmith", 4}, {"Barracks", 8}, {"Corn", 2}, {"Wheat", 2}, {"Mill", 5}, {"Fortress", 12}, {"Wall", 4}, {"WizardTower", 6}}; 
    public static GameObject activeobject;
    
    public void buildingclicked()
    {
        if(CameraScript.gold >= costs[gameObject.name])
        {
            activeobject = building; 
        }
        else
        {
            StartCoroutine(CameraScript.Shake(gameObject.transform.parent.gameObject));
        }
    }

    public void trained()
    {
        Dictionary <string, int> costs = new Dictionary<string, int>(){{"Archer", 4}, {"Knight1", 3}, {"Knight2", 6}, {"Knight3", 9}};
        if(CameraScript.gold >= costs[trainedunit.name])
        {
            CameraScript.gold -= costs[trainedunit.name];
            Instantiate(trainedunit, new Vector3(GameObject.Find("ActionTray").transform.position.x + 2.1f, BuildingScript.ylocation, -2), Quaternion.identity);
            GameObject.Find("ActionTray").transform.position = new Vector3(-53, 23, 0); 
        }
        else
        {
            StartCoroutine(CameraScript.Shake(gameObject));
        }
    }
}
