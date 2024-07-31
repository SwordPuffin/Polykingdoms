using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationScript : MonoBehaviour
{
    //Script that generates the world
    //Made By Nathan Perlman
    public GameObject grass, mountain;
    public List<GameObject> forestset, waterset, bush;
    float posx, posy, basex, basey = 0;
    public int worldsize, order;

    void Start()
    {
        for(int height = 0; height < worldsize; height++)
        {
            for(int width = 0; width < worldsize; width++)
            {
                int random = Random.Range(0, 1000);
                int select = Random.Range(0, 3);
                if(random < 10)
                {
                    Instantiate(waterset[select], new Vector3(posx, posy, 0), Quaternion.identity);
                    Instantiate(grass, new Vector3(posx, posy, 0), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = order;
                    order += 1;
                }
                else if(random >= 10 && random < 69)
                {
                    Instantiate(forestset[select], new Vector3(posx, posy, 0), Quaternion.identity);
                    Instantiate(grass, new Vector3(posx, posy, 0), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = order;
                    order += 1;
                }
                else if(random >= 69 && random < 75)
                {
                    GameObject mountainclone = Instantiate(mountain, new Vector3(posx, posy, 0), Quaternion.identity);
                    mountainclone.GetComponent<SpriteRenderer>().sortingOrder = order;
                    mountainclone.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order;
                    order += 1;
                }
                else
                {
                    GameObject block = Instantiate(grass, new Vector3(posx, posy, 0), Quaternion.identity);
                    block.GetComponent<SpriteRenderer>().sortingOrder = order;
                    if(random < 100)
                    {
                        GameObject decoration = Instantiate(bush[select], new Vector3(posx, posy + 0.4f, 0), Quaternion.identity);
                        decoration.transform.parent = block.transform;
                        decoration.GetComponent<SpriteRenderer>().sortingOrder = order;
                    }
                    order += 1;
                }
                posx += 1.2f;
                posy -= 0.7f;
            }
            basex -= 1.2f;
            basey -= 0.7f;
            posx = basex;
            posy = basey;
        }
    }
}
