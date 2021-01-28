using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurdenManager : MonoBehaviour
{
    [SerializeField] float maxBurdens = 4f;
    [SerializeField] float minMoveSpeedFactor = 0.75f;
    [SerializeField] float burdenFactor = 10f;
    [SerializeField] float numberOfBurdens = 0; //serialized for debugging only
    [SerializeField] GameObject burdenPrefab = default;

    ParticleSystem particles;
    float startingParticleScale;
    float startingMass;

    AudioManager audioManager;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        startingParticleScale = particles.transform.localScale.x;
        particles.transform.localScale = new Vector3(0,0,0);
        audioManager = FindObjectOfType<AudioManager>();
    }

    public float GetBurdenNumber()
    {
        return numberOfBurdens;
    }

    public void AddBurden()
    {
        numberOfBurdens += 1;
        SetBurdenedParameters();
        audioManager.Play("BurdenCollect");
    }

    public void DropBurden()
    {
        numberOfBurdens -= 1;
        CreateBurdenObject();
        SetBurdenedParameters();
        audioManager.Play("BurdenDrop");
    }

    public bool CanTakeBurden()
    {
        if (numberOfBurdens < maxBurdens)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetBurdenedParameters()
    {
        float burdenedSpeed = 1f - (numberOfBurdens * (1f - minMoveSpeedFactor) / maxBurdens);
        GetComponent<PlayerMover>().SetBurdenedSpeed(burdenedSpeed);
        
        if (numberOfBurdens == 0)
        {
            particles.transform.localScale = new Vector3(0,0,0);
        }
        else
        {
            float burdenedScale = startingParticleScale + (numberOfBurdens / burdenFactor);
            particles.transform.localScale = new Vector3(burdenedScale, burdenedScale, 1);
        }
    }

    private void CreateBurdenObject()
    {
        GameObject burden = Instantiate(burdenPrefab, transform.position, Quaternion.identity) as GameObject;
        burden.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
    }
}
