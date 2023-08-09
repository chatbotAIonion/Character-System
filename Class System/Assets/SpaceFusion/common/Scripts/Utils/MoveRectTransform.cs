using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SpaceFusion.common.Scripts.Utils
{
    public class MoveRectTransform : MonoBehaviour
    {
        public GameObject movedObject;
        public float duration;
        public bool loop;
        public GameObject[] goals;
        private void Start()
        {
            StartCoroutine(StartMovement());
        }

        private IEnumerator StartMovement()
        {
            do {
                foreach (var goal in goals)
                {
                    movedObject.transform.DOLocalMove(goal.transform.localPosition, duration).SetEase(Ease.Linear);
                    yield return new WaitForSeconds(duration);
                }
            }while (loop);
        }
    }
}