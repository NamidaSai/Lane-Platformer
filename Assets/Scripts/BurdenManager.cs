﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurdenManager : MonoBehaviour
{
    [SerializeField] float burdenFactor = 10f;
    [SerializeField] float numberOfBurdens = 0; //serialized for debugging only

    ParticleSystem particles;
    float startingParticleScale;
    float startingMass;


    private void Start()
    {
        startingMass = GetComponent<Rigidbody2D>().mass;
        particles = GetComponentInChildren<ParticleSystem>();
        startingParticleScale = particles.transform.localScale.x;
    }

    public void AddBurden()
    {
        numberOfBurdens += 1;
        SetBurdenedParameters();
    }

    public void RemoveAllBurdens()
    {
        numberOfBurdens = 0;
        SetBurdenedParameters();
    }

    private void SetBurdenedParameters()
    {
        GetComponent<Rigidbody2D>().mass = startingMass + (numberOfBurdens / burdenFactor);

        float burdenedScale = startingParticleScale + (numberOfBurdens / burdenFactor);
        particles.transform.localScale = new Vector3(burdenedScale, burdenedScale, 1);
    }
}