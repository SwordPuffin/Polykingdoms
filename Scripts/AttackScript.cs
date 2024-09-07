using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.transform.parent.name.Contains("Knight") || gameObject.transform.parent.name == "Archer(Clone)")
        {
            if(other.gameObject.name.Contains("Knight") || other.gameObject.name == "Archer(Clone)")
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if(gameObject.transform.parent.name.Contains("Tower"))
        {   
            if(other.gameObject.name == "Grass(Clone)" || other.gameObject.name == "Sand(Clone)" || other.gameObject.name == "Forest(Clone)" || other.gameObject.name == "ShallowWater(Clone)" || other.gameObject.name == "Water(Clone)")
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
            }
            if(other.gameObject.name.Contains("Knight") || other.gameObject.name == "Archer(Clone)")
            {
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }


        //if(other.gameObject.tag != MainMenuScript.colours[CameraScript.turn])
        // {
        // }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name.Contains("Knight") || other.gameObject.name == "Archer(Clone)")
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if(other.gameObject.name == "ShallowWater(Clone)")
        {
            other.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else if(other.gameObject.name == "Grass(Clone)" || other.gameObject.name == "Sand(Clone)" || other.gameObject.name == "Forest(Clone)" || other.gameObject.name == "Mountain(Clone)")
        {
            other.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if(other.gameObject.name == "Water(Clone)")
        {
            other.GetComponent<SpriteRenderer>().color = new Color32(95, 101, 159, 255);
        }
    }
}
