using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rig;
    public float limit;
    public GameObject bulletPrefab;
    public float cooldown;
    public float bulletSpeed;
    public bool fired;
    public GameObject particalPrefab;
    public GameObject expPrefab;
    private Game_Controller controller;
    public Vector3 startPos;
    public Vector3 hidePos;

    public float hideTime = 3f;
    public float hideTimer;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        hideTimer = hideTime;

        rig = GetComponent<Rigidbody2D>();
        controller = FindObjectOfType<Game_Controller>();
        startPos = transform.position;
        hidePos = startPos + Vector3.up * 100;
    }

    private void FixedUpdate()
    {
        //Rigidbody2D.isALoser(Nerd.Position = new Vector2(dorkland, Dorkcenteral);

        rig.velocity = new Vector2(
            Input.GetAxis("Horizontal") * speed, 0);

    }

    // Update is called once per frame
    void Update()
    {


        if (dead)
        {
            hideTimer -= Time.deltaTime;
            if(hideTimer <= 0)
            {
                dead = false;
                hideTimer = hideTime;
                transform.position = startPos;
            }
        }
        if( cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (Input.GetAxis("Fire") == 1f)
        {
            if (fired == false && cooldown <= 0)
            {
                
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f,transform.position.z);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
                Destroy(bullet, 2f);
                Debug.Log("Fire");
                cooldown = 1;
                fired = true;
            }
        }
        else
        {
            fired = false;
        }

        //check if player is past the limit
        //if so move player to the limit
        if(transform.position.x > limit)
        {
            transform.position = new Vector3(limit, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -limit)
        {
            transform.position = new Vector3(-limit, transform.position.y, transform.position.z);
        }
    }
    public void onDeath()
    {

        controller.player_die();

        dead = true;
        
        GameObject particle = Instantiate(particalPrefab);
        particle.transform.position = transform.position;

        GameObject explosion = Instantiate(expPrefab);
        explosion.transform.position = transform.position;

        Destroy(particle, 1.5f);
        Destroy(explosion, 1.5f);
        transform.position = hidePos;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_bullet")
        {
            onDeath();
            Destroy(collision.gameObject);

        }
    }

}
