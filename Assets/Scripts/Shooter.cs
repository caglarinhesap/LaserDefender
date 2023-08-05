using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minimumFireRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.velocity = transform.up * projectileSpeed;
            }
            Destroy(projectile, projectileLifeTime);

            float fireDelay = UnityEngine.Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            fireDelay = Mathf.Clamp(fireDelay, minimumFireRate, float.MaxValue);
            yield return new WaitForSeconds(fireDelay);
        }
    }
}
