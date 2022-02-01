using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject explosion;
    public PlayerHealthbarScript playerHealthbar;
    public float speed = 10f;
    public float padding = 0.8f;
    float minX;
    float maxX;
    float minY;
    float maxY;

    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;

    void Start()
    {
        FindBoundaries();
        damage = barFillAmount / health;
    }
    void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+ padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x- padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    
    void Update()
    {
        float deltaY= Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float deltaX = Input.GetAxis("Horizontal")* Time.deltaTime * speed;

        float newXpos= Mathf.Clamp(transform.position.x + deltaX,minX,maxX);
        float newYpos=Mathf.Clamp(transform.position.y + deltaY,minY,maxY);

        transform.position = new Vector2(newXpos, newYpos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="EnemyBullet")
        {
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            if (health<=0)
            {
                Destroy(gameObject);
                GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(blast, 2f);
            }

        }
    }
    void DamagePlayerHealthbar()
    {
        if(health>0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthbar.SetAmount(barFillAmount);
        }
    }
}
