using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{
    const string animationName = "TakeDamage";

    public int health;

    private Animator animator;
    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        audioSource.Play();
        animator.SetTrigger(animationName);

        StartCoroutine(DecreseHealth(damage));
    }

    private IEnumerator DecreseHealth(int damage)
    {
        yield return Utils.WaitForAnimationEnds(animator, animationName);

        health -= damage;
        if (health == 0)
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(gameObject);
    }
}