using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    //Script that manages instantiated walls
    //Made by Nathan Perlman 
    public List<Sprite> wallpieces;
    public GameObject wall, left, right, up, down;
    bool triggered;
    
    void Start()
    {
        triggered = false;
    }
    public void Update()
    {
        gameObject.transform.Rotate(0, 0, 0.001f);
        if(wall.name == "Wall")
        {
            triggered = false;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Wall(Clone)")
        {
            triggered = true;
        }
        if(left.GetComponent<WallScript>().triggered || right.GetComponent<WallScript>().triggered)
        {
            wall.GetComponent<SpriteRenderer>().sprite = wallpieces[0];
        }
        if(up.GetComponent<WallScript>().triggered || down.GetComponent<WallScript>().triggered)
        {
            wall.GetComponent<SpriteRenderer>().sprite = wallpieces[1];
        }
        if(left.GetComponent<WallScript>().triggered && down.GetComponent<WallScript>().triggered)  
        {
            wall.GetComponent<SpriteRenderer>().sprite = wallpieces[2];
        } 
        if(right.GetComponent<WallScript>().triggered && down.GetComponent<WallScript>().triggered)
        {
            wall.GetComponent<SpriteRenderer>().sprite = wallpieces[3];
        }
        if(left.GetComponent<WallScript>().triggered && up.GetComponent<WallScript>().triggered)
        {
            wall.GetComponent<SpriteRenderer>().sprite = wallpieces[4];
        }
        if(right.GetComponent<WallScript>().triggered && up.GetComponent<WallScript>().triggered)
        {
            wall.GetComponent<SpriteRenderer>().sprite = wallpieces[5];
        }
    }
}
