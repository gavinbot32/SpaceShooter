using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject particalPrefab;
    public GameObject expPrefab;
    public int points;
    private Game_Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<Game_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            controller.addScore(points);
            GameObject particle = Instantiate(particalPrefab);
            particle.transform.SetParent(transform.parent.parent);
            particle.transform.position = transform.position;

            GameObject explosion = Instantiate(expPrefab);
            explosion.transform.SetParent(transform.parent.parent);
            explosion.transform.position = transform.position;

            Destroy(particle, 1.5f);
            Destroy(explosion, 1.5f);
            

            Destroy(collision.gameObject);
            Destroy(gameObject,0.1f);
        }
    }

}
