using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class CircleControl : MonoBehaviour
    {
        public float sensDuration;
        public float disableDuration;
        private float currentTime;
        private Arrow arrow;
        private CircleThrower circleThrower;
        private WaitForSeconds wait;
        private AudioSource audioSource;

        private void Start()
        {
            circleThrower = transform.root.GetComponent<CircleThrower>();
            wait = new WaitForSeconds(disableDuration);
            audioSource = circleThrower.GetComponent<AudioSource>();
            StartCoroutine(CircleDisableTimer());
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Arrow"))
            {
                if (!arrow)
                    arrow = collision.gameObject.GetComponent<Arrow>();

                if (!arrow.isUsed)
                {
                    if ((currentTime + sensDuration) < Time.time)
                    {
                        if (arrow.enabled)
                        {
                            audioSource.Play();
                            arrow.scoreManager.UpdateScore();
                            arrow.isUsed = true;
                            GameManager.GetInstance.ScoreEffect(arrow.transform.position);
                            arrow.launcher.MoveArrowUsedPool(arrow.gameObject);
                            circleThrower.MoveCircleUsedPool(gameObject);
                        }
                    }
                }

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Arrow"))
            {
                currentTime = Time.time;
            }
        }

        IEnumerator CircleDisableTimer()
        {
            yield return wait;
            circleThrower.MoveCircleUsedPool(gameObject);
        }
    }
}
