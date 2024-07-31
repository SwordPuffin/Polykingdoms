using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour
{
    Dictionary<string, int> upgrades = new Dictionary<string, int>(){{"Barracks(Clone)", 7}, {"ArcherTower(Clone)", 8}, {"WizardTower(Clone)", 12}, {"Fortress(Clone)", 12}, {"Castle(Clone)", 20}}; 
    public List<Sprite> sprites;
    public GameObject warning;
    private int counter = -1;
    private bool active;
    void Update()
    {
        if(active)
        {
            warning.transform.position = new Vector3(transform.position.x, transform.position.y + 1.8f, 0);
        }
    }
    void OnMouseDown()
    {
        if(CameraScript.gold >= upgrades[gameObject.name] && CameraScript.unlocked[gameObject.name.Replace("(Clone)", "")] > counter + 1)
        {
            warning.GetComponent<Button>().onClick.AddListener(confirm);
            active = true;
        }
    }
    public void confirm()
    {
        counter += 1;
        active = false;
        CameraScript.gold -= upgrades[gameObject.name];
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[counter]; 
        warning.transform.position = new Vector3(-53, 23, 0);  
        warning.GetComponent<Button>().onClick.RemoveListener(confirm);
    }
} 
