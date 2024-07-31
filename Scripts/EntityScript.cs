using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    //Script that handles game entities movement and attacking
    //Made by Nathan Perlman
    public GameObject veil;
    Vector3 originpoint;
    GameObject lockpoint;
    void OnMouseDrag()
    {
        lockpoint = PlaceScript.location;
        if(lockpoint.GetComponent<SpriteRenderer>().color == Color.grey)
        {
            gameObject.transform.position = new Vector3(lockpoint.transform.position.x, lockpoint.transform.position.y + 0.5f, 0);
        }
        veil.transform.position = originpoint;
    }
    void OnMouseUp()
    {
        veil.GetComponent<CircleCollider2D>().isTrigger = false;
    }
    void OnMouseDown()
    {
        veil.GetComponent<CircleCollider2D>().isTrigger = true;
        originpoint = gameObject.transform.position;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Grass(Clone)" || other.gameObject.name == "Sand(Clone)" || other.gameObject.name == "Forest(Clone)" || other.gameObject.name == "Mountain(Clone)")
        {
            other.GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Grass(Clone)" || other.gameObject.name == "Sand(Clone)" || other.gameObject.name == "Forest(Clone)" || other.gameObject.name == "Mountain(Clone)")
        {
            other.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
