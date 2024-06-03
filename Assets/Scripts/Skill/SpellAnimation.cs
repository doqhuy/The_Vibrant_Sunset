using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAnimation : MonoBehaviour
{
    public string AnimationName = "Empty";
    public enum AnimationType { Cast, Explode}
    public AnimationType Type;
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(AnimationName);
        if(Type == AnimationType.Explode)
        {
            StartCoroutine(EndOfExplosion());
        }
    }

    IEnumerator EndOfExplosion()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
