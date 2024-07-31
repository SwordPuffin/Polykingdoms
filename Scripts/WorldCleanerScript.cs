using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldCleanerScript : MonoBehaviour
{
    //Cleans up issues created during the world generation
    //Made by Nathan Perlman
    public GameObject sand;
    float time = 0;

    void Update()
    {
        if(time > 3)
        {
            if(gameObject.transform.parent != null)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            Destroy(GetComponent<PolygonCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<WorldCleanerScript>());
        }
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.name == "Water(Clone)" && other.gameObject.name == "Grass(Clone)")
        {
            Instantiate(sand, new Vector3(other.transform.position.x, other.transform.position.y, 0), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder;
            Destroy(other.gameObject);
        }
    }
}
