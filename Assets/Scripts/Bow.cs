using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Animator anim;

    private bool bowCharge = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && timeBtwShots <= 0 && !bowCharge)
        {
            anim.Play("BowCharge", -1, 0.25f);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && bowCharge)
        {
            anim.Play("BowShoot", -1, 0.25f);
        }    
    }

    public void SpawnArrow()
    {
        Instantiate(arrow, shotPoint.position, transform.rotation);
        bowCharge = false;
    }

    public void BowIsCharged()
    {
        bowCharge = true;
    }
}
