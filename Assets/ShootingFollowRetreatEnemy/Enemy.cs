using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    
    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform turretPoint;
    public Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

        turretPoint = transform.GetChild(0);
    }

    void Update() {
        float distance2Player = Vector2.Distance(transform.position, player.position);

        // Follows the Charactor and Retreat when to close to him
        if (distance2Player > stoppingDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else if (distance2Player < stoppingDistance && distance2Player > retreatDistance) {
            // Don't move ~ keep safe distance 
        } else if (distance2Player < retreatDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }


        // Weapon direction follow Charactor
        Vector2 difference = player.position - transform.position; 
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Atan2: tang của vector difference __ Rad2Deg chuyển từ radian sang độ
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        // Shooting Enemy
        if (timeBtwShots <= 0) {
            Instantiate(projectile, turretPoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
