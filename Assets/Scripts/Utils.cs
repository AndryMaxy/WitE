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

    public static Vector2 DefineDiraction2D(Quaternion rotation)
    {

        if ((rotation.y == 0 && rotation.z == 0))
        {
            return Vector2.right;

        }
        if ((rotation.y < 0 && rotation.z == 0))
        {
            return Vector2.left;

        }
        if (rotation.y == 0 && rotation.z <= 0)
        {
            return Vector2.down;

        }

        return Vector2.up;
    }

}