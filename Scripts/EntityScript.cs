using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public GameObject veil, arrow, ball;
    public int hearts;
    GameObject lockpoint, currentlockpoint, attacker, defender;
    bool moved, shoot, hit;

    void Update()
    {
        if(moved && gameObject.GetComponent<SpriteRenderer>().color == Color.white)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(lockpoint.transform.position.x, lockpoint.transform.position.y + 0.2f, -2), 10 * Time.deltaTime);  
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10000;
            if(transform.position == new Vector3(lockpoint.transform.position.x, lockpoint.transform.position.y + 0.2f, -2))
            {
                currentlockpoint = lockpoint;
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = lockpoint.GetComponent<SpriteRenderer>().sortingOrder + 1;  
                currentlockpoint.GetComponent<PlaceScript>().unitontile = true;
                moved = false;
                if(lockpoint.name == "ShallowWater(Clone)" || lockpoint.name == "Water(Clone)")
                {
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        if(shoot && BuildingScript.activetower.name.Contains("Archer"))
        {
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, gameObject.transform.position, 8 * Time.deltaTime);
            arrow.transform.rotation = Quaternion.Lerp(arrow.transform.rotation, Quaternion.LookRotation(Vector3.forward, gameObject.transform.position - arrow.transform.position), Time.deltaTime * 10);
            if(Vector3.Distance(arrow.transform.position, transform.position) < 0.5f)
            {
                BuildingScript.activetower = null;
                arrow.transform.position = new Vector3(-57.2f, 36.8f, 0); 
                shoot = false;
            }
        }
        if(defender != null)
        {
            Debug.Log("hit");
            attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, defender.transform.position, 4 * Time.deltaTime);
            gameObject.transform.GetChild(1).GetComponent<Collider2D>().isTrigger = false;
            if(attacker.transform.position == defender.transform.position)
            {
                hit = false;
                attacker = null;
                defender = null;
            }
            
            // if(Vector3.Distance(attacker.transform.position, gameObject.transform.position) < 0.1f)
            // {
            //     hit = true;
            // }
            // if(!hit)
            // {
            //     Debug.Log("hit1");
            // }
            // else 
            // {
            //     attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, new Vector3(currentlockpoint.transform.position.x, currentlockpoint.transform.position.y + 0.2f, -2), Time.deltaTime * 4);  

            // }
        }
        if(hearts <= 0 && !shoot)
        {
            currentlockpoint.GetComponent<PlaceScript>().unitontile = false;
            Destroy(gameObject);
        }
    }
    void OnMouseUp()
    {
        lockpoint = PlaceScript.location;
        if(lockpoint.GetComponent<SpriteRenderer>().color == Color.grey && !moved)
        {
            moved = true;
            if(currentlockpoint != null)
            {
                currentlockpoint.GetComponent<PlaceScript>().unitontile = false;
            }
        }
        gameObject.transform.GetChild(1).GetComponent<Collider2D>().isTrigger = false;
        veil.GetComponent<CircleCollider2D>().isTrigger = false;
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(gameObject.GetComponent<SpriteRenderer>().color == Color.white)
            {
                gameObject.transform.GetChild(1).GetComponent<Collider2D>().isTrigger = !gameObject.transform.GetChild(1).GetComponent<Collider2D>().isTrigger;
                if(gameObject.transform.GetChild(1).GetComponent<Collider2D>().isTrigger)
                {
                    attacker = gameObject;
                }
                else 
                {
                    attacker = null;
                }
            }
            else if(gameObject.GetComponent<SpriteRenderer>().color == Color.green)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.GetComponent<EntityScript>().hearts -= 1; 
                shoot = true;
            }
            else if(gameObject.GetComponent<SpriteRenderer>().color == Color.red)
            {
                gameObject.GetComponent<EntityScript>().hearts -= 1; 
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                defender = gameObject;
            }
        }
        if(Input.GetMouseButtonDown(0) && !moved && gameObject.GetComponent<SpriteRenderer>().color == Color.white && BuildingScript.activetower == null)
        {
            veil.GetComponent<CircleCollider2D>().isTrigger = true;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Grass(Clone)" && !other.GetComponent<PlaceScript>().unitontile || other.gameObject.name == "Sand(Clone)" && !other.GetComponent<PlaceScript>().unitontile || other.gameObject.name == "Forest(Clone)" && !other.GetComponent<PlaceScript>().unitontile || other.gameObject.name == "ShallowWater(Clone)" && !other.GetComponent<PlaceScript>().unitontile || other.gameObject.name == "Water(Clone)" && !other.GetComponent<PlaceScript>().unitontile && gameObject.transform.GetChild(1).gameObject.activeSelf)
        {
            other.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if(other.gameObject.name == "Mountain(Clone)" && !other.GetComponent<PlaceScript>().unitontile)
        {
            other.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "ShallowWater(Clone)")
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
