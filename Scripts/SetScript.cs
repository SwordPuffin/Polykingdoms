using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScript : MonoBehaviour
{
    //Script for setting grass blocks to water, forest or mountain
    //Made by Nathan Perlman
    public GameObject water;
    public List<GameObject> forest;
    float time = 0;
    bool first, second;
    void Update()
    {
        time += Time.deltaTime;
        if(gameObject.name == "Waterset(Clone)" && time > 1 && !first)
        {
            gameObject.transform.Rotate(0, 0, Random.Range(0, 360));
            first = true;
        }
        else if(gameObject.name == "Forestset(Clone)" && time > 2 && !second)
        {
            gameObject.transform.Rotate(0, 0, Random.Range(0, 360));
            second = true;
        }
        if(time > 3)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Grass(Clone)" || gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Mountain(Clone)" || gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Forest(Clone)" || gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Sand(Clone)")
        {
            Instantiate(water, new Vector3(other.transform.position.x, other.transform.position.y, 0), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder;
            Destroy(other.gameObject);
        }
        if(gameObject.name == "Forestset(Clone)" && other.gameObject.name == "Grass(Clone)" && time > 2)
        {
            int random = Random.Range(0, 2);
            GameObject tree = Instantiate(forest[random], new Vector3(other.transform.position.x, other.transform.position.y, 0), Quaternion.identity);
            tree.GetComponent<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder;
            tree.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = tree.GetComponent<SpriteRenderer>().sortingOrder;
            Destroy(other.gameObject);
        }
    }
}
