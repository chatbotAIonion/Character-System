using System.Collections;
using UnityEngine;

namespace SpaceFusion.common.Scripts.Utils
{
    public class StartAnimation : MonoBehaviour
    {

        public Animator animator;
        public new RuntimeAnimatorController animation;
        [Header("value in seconds")]
        public float waitUntilStart;


        private void OnEnable()
        {
            animator.enabled = false;
            StartCoroutine(PlayAnimation());
        }


        private IEnumerator PlayAnimation()
        {
            yield return new WaitForSeconds(waitUntilStart);
            animator.enabled = true;
            animator.runtimeAnimatorController = animation;
            animator.Play(animation.name);
        }
    }
}