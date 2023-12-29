using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;

    [Header("Local References")]
    public ParticleSystem enable_Particle;
    public ParticleSystem disable_Particle;
    public GameObject model;

    [HideInInspector]
    public int value;

    [Header("Private References")]
    private float actualMovingSpeed;
    private Collider col;

    private Coroutine disableCoroutine;

    private void Awake()
    {
        col = GetComponent<Collider>();
        disableCoroutine = null;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Wall"))
        {
            col.transform.GetComponent<WallBehaviour>().TakeHit(value);
            Disable();
        }

        if (col.transform.CompareTag("Interactable"))
        {
            col.transform.GetComponent<Interactable>().TakeHit(this.gameObject);
            col.transform.GetComponent<Interactable>().TakeHit(value);

            if (col.transform.GetComponent<Interactable>().disableBulletOnHit)
            {
                Disable();
            }
        }

        if (col.transform.CompareTag("MidSegment"))
        {
            StopAllCoroutines();
            transform.DOPause();
            transform.DOKill();
        }

        if (col.transform.CompareTag("EndGame"))
        {
            StopAllCoroutines();
            transform.DOPause();
            transform.DOKill();

            EndGameBehaviour.Instance.TakeBulletHit(value);
            Disable();
        }
    }

    /// <summary>
    /// Set New value to Bullet than Activate It
    /// </summary>
    public void Init()
    {
        Enable();
    }

    /// <summary>
    /// Set New value to Bullet than Activate It
    /// </summary>
    /// <param name="newValue"><bullet value /param>
    public void Init(int newValue)
    {
        value = newValue;

        Enable();
    }

    /// <summary>
    /// Activate the bullet
    /// </summary>
    public void Enable()
    {
        this.gameObject.SetActive(true);

        actualMovingSpeed = moveSpeed;
        transform.localScale = Vector3.one;
        model.gameObject.SetActive(true);
        col.enabled = true;

        enable_Particle.Play();

        disableCoroutine = StartCoroutine(DisableCoroutine(CharacterBehaviour.instance.characterShooter.bulletActiveTime));
        StartCoroutine(MoveForward());
    }

    public void Disable()
    {
        actualMovingSpeed = 0;
        model.gameObject.SetActive(false);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        col.enabled = false;

        disable_Particle.Play();

        StartCoroutine(DisableObjectWithDelay(1));
    }

    /// <summary>
    /// Start Disable Sequence When Bullet is Spawned
    /// </summary>
    /// <param name="i_TimeToDisable"></param>
    /// <returns></returns>
    private IEnumerator DisableCoroutine(float i_TimeToDisable)
    {
        yield return new WaitForSeconds(i_TimeToDisable);

        Disable();
    }

    /// <summary>
    /// Game object Actual Disabler
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableObjectWithDelay(float i_delay)
    {
        yield return new WaitForSeconds(i_delay);

        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Start Moving Forward OnEnable
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveForward()
    {
        yield return new WaitForSeconds(0.035f);

        while (this.gameObject.activeInHierarchy)
        {
            transform.Translate(transform.forward * actualMovingSpeed * Time.deltaTime);
            yield return null;
        }
    }
}