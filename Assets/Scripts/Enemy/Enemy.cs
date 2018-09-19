using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float speed;
    public float activeDistance;
    public float stoppingDistance;
    public float retreatDistance;
    public bool patrol = true;

    private bool movingRight = true;
    [SerializeField]
    private GameObject player;
    //[SerializeField]
    //private GameObject _fistPrefab;
    public Transform groundDetection;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < activeDistance)
        {
            patrol = false;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > activeDistance)
        {
            patrol = true;
        }

        if (patrol)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
            if (groundInfo.collider == false)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
        if (!patrol)
        {
            //rotate to look at the player
            if (Vector2.Distance(transform.position, player.transform.position) < 0)
            {
                movingRight = false;
            }

            if (Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
            {

                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            /*if(movingRight)
                Instantiate(_fistPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
            if(!movingRight)
                Instantiate(_fistPrefab, transform.position - new Vector3(1, 0, 0), Quaternion.identity);
                */
            player.GetComponent<Player>();

        }
    }
}


