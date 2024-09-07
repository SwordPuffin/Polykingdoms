using System.Collections.Generic;
using System.Collections;
using UnityEngine;



public class WorldCleanerScript : MonoBehaviour
{
    //Script that makes the world more elaborate (needs to be optimized, like really badly)
    //Made by Nathan Perlman
    public Sprite sand;
    public GameObject water;
    public List<GameObject> forest;
    private bool generateshallowwater;

    void Start()
    {
        StartCoroutine(enable());
    }
    IEnumerator enable()
    {
        if(gameObject.name.Contains("set"))
        {
            GameObject[] objectstoenable = GameObject.FindGameObjectsWithTag("Setter");
            foreach (GameObject obj in objectstoenable)
            {
                obj.GetComponent<Collider2D>().enabled = true;
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.Rotate(0, 0, 0.0001f);
        generateshallowwater = true;
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<WorldCleanerScript>());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.name.Contains("Water(Clone)") && other.gameObject.name == "Grass(Clone)" || gameObject.name.Contains("Water(Clone)") && other.gameObject.name == "Forest(Clone)" || gameObject.name.Contains("Water(Clone)") && other.gameObject.name == "Sand(Clone)")
        {
            other.gameObject.name = "Sand(Clone)";
            other.gameObject.GetComponent<SpriteRenderer>().sprite = sand;
            if(other.gameObject.transform.childCount == 1)
            {
                Destroy(other.gameObject.transform.GetChild(0).gameObject);
            }
        }
        if(gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Grass(Clone)" || gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Mountain(Clone)" || gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Forest(Clone)" || gameObject.name == "Waterset(Clone)" && other.gameObject.name == "Sand(Clone)")
        {
            Instantiate(water, new Vector3(other.transform.position.x, other.transform.position.y, 0), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder;
            Destroy(other.gameObject);
        }
        else if(gameObject.name == "Forestset(Clone)" && other.gameObject.name == "Grass(Clone)")
        {
            int random = Random.Range(0, 2);
            forest[random].GetComponent<SpriteRenderer>().sortingOrder = other.GetComponent<SpriteRenderer>().sortingOrder;
            forest[random].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = forest[random].GetComponent<SpriteRenderer>().sortingOrder;
            Instantiate(forest[random], new Vector3(other.transform.position.x, other.transform.position.y, 0), Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(gameObject.name == "Water(Clone)" && other.gameObject.name == "Sand(Clone)" && generateshallowwater || gameObject.name == "Water(Clone)" && other.gameObject.name == "Mountain(Clone)" && generateshallowwater)
        {
            gameObject.name = "ShallowWater(Clone)";
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }
}
