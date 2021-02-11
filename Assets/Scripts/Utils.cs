using UnityEngine;
using System.Collections;


public class Utils
{

    public static IEnumerator WaitForAnimationEnds(Animator animator, string animationName)
    {
        yield return new WaitUntil(() =>
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= stateInfo.length;
        });
    }

}