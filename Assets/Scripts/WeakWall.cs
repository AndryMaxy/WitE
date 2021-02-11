using UnityEngine;
using System.Collections;


public class WeakWall : MonoBehaviour, IExplosive
{
    const string animationName = "ExplodeWall";

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void explode()
    {
        animator.SetTrigger(animationName);

        StartCoroutine(explodeWall());
    }

    private IEnumerator explodeWall()
    {
        yield return Utils.WaitForAnimationEnds(animator, animationName);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}