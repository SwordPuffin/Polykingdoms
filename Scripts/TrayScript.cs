using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayScript : MonoBehaviour
{
    //Scipt that is used when the buttons in the panel are clicked
    //Made by Nathan Perlman
    public GameObject building;
    public static GameObject activeobject;
    
    public void buildingclicked()
    {
        activeobject = building; 
    }
    void OnDisable()
    {
        building.SetActive(false);
    }
    void OnEnable()
    {
        building.SetActive(true);
    }
}
