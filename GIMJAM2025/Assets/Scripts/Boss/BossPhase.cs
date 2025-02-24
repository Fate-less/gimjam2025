using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : Enemy, IDamagable
{
    public float spawnMobsCooldown;
    public float spinAttackCooldown;
    public float bombAttackCooldown;
    public GameObject BOSS_SpawnMob;
    public GameObject BOSS_SpinAttack;
    public GameObject BOSS_BombAttack;

    private float spawnMobsTimer;
    private float spinAttackTimer;
    private float bombAttackTimer;
    private float timeFlee;

    void Update()
    {
        timeFlee += Time.deltaTime;

        spawnMobsTimer += Time.deltaTime;
        if (spawnMobsTimer >= spawnMobsCooldown)
        {
            Debug.Log("Mobs Spawned");
            Instantiate(BOSS_SpawnMob, new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);
            spawnMobsTimer = 0f;
        }

        if (Health <= 60)
        {
            spinAttackTimer += Time.deltaTime;
            if (spinAttackTimer >= spinAttackCooldown)
            {
                Instantiate(BOSS_SpinAttack, transform.position, Quaternion.identity);
                spinAttackTimer = 0f;
            }
        }

        if (Health <= 30)
        {
            bombAttackTimer += Time.deltaTime;
            if (bombAttackTimer >= bombAttackCooldown)
            {
                Instantiate(BOSS_BombAttack, transform.position, Quaternion.identity);
                bombAttackTimer = 0f;
            }
        }
    }
}