using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bunker : MonoBehaviour
{

    public Transform[] cubes;
    private bool hitBool = false;
    private int  hitTimer = 120;
    public int hits = 3;
    // Start is called before the first frame update
    void Start()
    {
        cubes = gameObject.GetComponentsInChildren<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hitBool)
        {
            hitTimer--;
            print(hitTimer);
            if(hitTimer <= 0)
            {
                hitBool = false;
                hitTimer = 120;
            }
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitBool)
        {
            if (collision.CompareTag("Bullet") || collision.CompareTag("Enemy_bullet"))
            {
                hits--;
                if(hits <= 0)
                {
                    Destroy(gameObject);
                }
                Transform[] blocksAllive;
                blocksAllive = gameObject.GetComponentsInChildren<Transform>();
                
                for(int i = 1; i < 30; i++)
                {
                    int x = Random.Range(1, blocksAllive.Length);
                    if (blocksAllive[x] != null)
                    {
                       
                        Destroy(blocksAllive[x].gameObject);
                    }
                }

                hitBool = true;
                Destroy(collision.gameObject);
              

            }
        }
        if (collision.tag == "Enemy")
        {
            print("enemy Enter");
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }




}
