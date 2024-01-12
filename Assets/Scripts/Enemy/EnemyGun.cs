using Assets.Scripts.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGun : MonoBehaviour
{
    protected float timeSinceLastShot;
    [Header("References")]
    [SerializeField] protected GunData gunData;
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected GunSounds gunSounds;
    [SerializeField] protected AbstractEnemyData enemyData;
    [SerializeField] protected GameObject m_shotPrefab;
    public Transform player;

    private void Start()
    {
        gunSounds = GetComponent<GunSounds>();
        enemyData = GetComponent<AbstractEnemyData>();
        player = GameObject.Find("Player").transform;
    }

    protected bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (enemyData.GetHealth() <= 0)
            return;
        transform.LookAt(player.transform.position);
        if (CanShoot() && Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, gunData.maxDistance) && hit.transform.tag != "Enemy")
        {
            GameObject laser = Instantiate(m_shotPrefab, muzzle.position, muzzle.rotation);
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            damageable?.Damage(gunData.damage);
            Destroy(laser, 2f);
            OnGunShot();
        }

    }
    private void OnGunShot()
    {
        timeSinceLastShot = 0;
        gunSounds.PlayShootSound();
    }
    private void Update() => timeSinceLastShot += Time.deltaTime;

}