using DG.Tweening;
using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    [Header("Variables")]
    public float fireRate;
    public float bulletActiveTime;

    [Header("Special Shoot Variables")]
    public float spreadShootBulletRotation;

    [Header("Project References")]
    public GameObject shootPosition;

    [Header("Project References")]
    public GameObject bulletPrefs;

    [Header("Private Variables")]
    private bool spreadShoot;
    private Coroutine shootCoroutine;

    private CharacterBehaviour characterBehaviour;

    private void Awake()
    {
        characterBehaviour = CharacterBehaviour.Instance;

        spreadShoot = false;

        shootCoroutine = null;
    }

    /// <summary>
    /// Start Player Shoot Behaviour
    /// </summary>
    public void StartShoot()
    {
        if(shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    public void StopShoot()
    {
        if(shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    /// <summary>
    /// Shoot Coroutine , each X seconds Shoot a bullet
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootCoroutine()
    {
        while(true)
        {
            if (!spreadShoot)
            {
                Shoot(new Quaternion(0, 0, 0, 0));
            }

            else
            {
                for (int i = 0; i < 3; i++)
                {
                    ///Set 3 different rotation for different bullets
                    Vector3 rot = Vector3.zero;
                    if (i == 0)
                        rot = new Vector3(0, -spreadShootBulletRotation, 0);
                    else if(i == 1)
                        rot = new Vector3(0, 0, 0);
                    else
                        rot = new Vector3(0, spreadShootBulletRotation, 0);

                    Shoot(Quaternion.Euler(rot));
                }
            }

            yield return new WaitForSeconds(fireRate);
        }
    }

    /// <summary>
    /// Shoot New Bullet
    /// </summary>
    private void Shoot(Quaternion i_rot)
    {
        if (DeviceCapabilities.isVersionSupported)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }

        GameObject bullet = PoolManager.instance.GetPooledItem(bulletPrefs, shootPosition.transform.position);
        bullet.transform.rotation = i_rot;    
        bullet.GetComponent<BulletBehaviour>().Init();
    }

    /// <summary>
    /// Enable Spread Shoot Behaviour
    /// </summary>
    public void EnableSpreadShot()
    {
        spreadShoot = true;
    }

    /// <summary>
    /// Increase/Decrease Fire Rate
    /// </summary>
    /// <param name="i_amount"><Amount to Increase or Decrease Fire Rate /param>
    public void ChangeFireRate(float i_amount)
    {
        fireRate += i_amount;

        if (i_amount < 0)
            characterBehaviour.powerUpParticle.Play();
        else
            characterBehaviour.powerDownParticle.Play();
    }

    /// <summary>
    /// Increase/Decrease Fire Disatance
    /// </summary>
    /// <param name="i_amount"><Amount to Increase or Decrease Fire Distance /param>
    public void ChangeFireDistance(float i_amount)
    {
        bulletActiveTime += i_amount;

        if (i_amount < 0)
            characterBehaviour.powerUpParticle.Play();
        else
            characterBehaviour.powerDownParticle.Play();
    }
}
