﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public Transform gunEnd;
    public float maxRange = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float reloadTime = 0.5f;
    public bool seePlayer = false;
    private AudioSource myAudioSource;
    public AudioClip shootSound;


    private bool isFiring = false;

    private void SetNotFiring()
    {
        isFiring = false;
    }

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        int layerMaskPlayer = 1 << 10;
        int layerMaskObstacle = 1 << 0;

        int finalLayerMask = layerMaskObstacle | layerMaskPlayer;

        RaycastHit2D hit = Physics2D.Raycast(gunEnd.position, gunEnd.up, maxRange, finalLayerMask);
        Debug.DrawRay(gunEnd.position, gunEnd.up, Color.red);

        if (hit.collider != null) 
        {
            if (hit.collider.tag == "Player" && !isFiring)
            {
                Fire();
            } 

            if (hit.collider.tag == "Player")
            {
                seePlayer = true;
            }
            else
            {
                seePlayer = false;
            }
        }
	}


    private void Fire()
    {
        isFiring = true;
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        myAudioSource.PlayOneShot(shootSound, 0.2f);

        Invoke("SetNotFiring", reloadTime);
    }

    public bool GetSeePlayer()
    {
        return seePlayer;
    }
}
