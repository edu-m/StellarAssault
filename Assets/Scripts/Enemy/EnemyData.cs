using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour, IDamageable
{
    private Animator animator;
    private ParticleSystem ps;
    
    [SerializeField] public float health;
    

    public void Spawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        gameObject.SetActive(true);
    }
    public void Damage(float damage)
    {
        Debug.Log("Damage");
        health -= damage;
        if (health <= 0)
            DeathEvent();
    }

    IEnumerator DeathAnimationFade()
    {
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5f);
        //ps.gameObject.SetActive(true);
        ps.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    public void DeathEvent()
    {
        StartCoroutine(DeathAnimationFade());
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        //ps.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

}
