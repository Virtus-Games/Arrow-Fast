using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class Arrow : MonoBehaviour
    {
        public float offsetOnWall;
        public float strafeSpeed;
        public float strafeAmplitude;
        public float rotateSpeed;
        public bool isThrowed;
        public bool isCatched;
        public bool isUsed;
        public TrailRenderer[] trailRenderers;
        private Transform tr; 
        [HideInInspector]
        public BoxScoreManager scoreManager;

        private Rigidbody rb;
        private ScreenHandler screenHandler;
        private float deathTime;

         [HideInInspector]
        public ArrowThrower launcher;


        public  float timer;

 


        void Init()
        {

            tr = transform;
            rb = GetComponent<Rigidbody>();
            screenHandler = FindObjectOfType<ScreenHandler>();
            launcher = tr.root.GetComponent<ArrowThrower>();
            deathTime = GameManager.GetInstance.arrowSO.disableDuration; 
        }

        private void Start()
        {
            Init();
        }
        void Update()
        {
            HandleArrow();


        }

        /// <summary>
        /// Oklarýn trail efektlerini etkinleþtirir.
        /// </summary>
        public void OpenTrails()
        {
            foreach (var trail in trailRenderers)
            {
                trail.enabled = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Target"))
            {
                scoreManager = collision.gameObject.GetComponent<BoxScoreManager>();
                MaterialAnim materialAnim = collision.gameObject.GetComponent<MaterialAnim>();
                materialAnim.centerUV.x = screenHandler.UVCord.x;
                materialAnim.centerUV.y = screenHandler.UVCord.y;
                 materialAnim.HandleMaterialAnim();
                rb.isKinematic = true;
                tr.localRotation = Quaternion.identity;
                isCatched = true;


            }

        }

        /// <summary>
        /// Oklarýn atýlmadan önceki ve sonraki dönme ve strafe hareketini iþler.
        /// </summary>
        void HandleArrow()
        {
            if (!isCatched)
            {
                if (!isThrowed)
                {
                    tr.localPosition = new Vector3(0, Mathf.Abs(Mathf.Sin(Time.time * strafeSpeed) * strafeAmplitude), 0);
                    tr.Rotate(-Vector3.forward, rotateSpeed / 10 * Time.deltaTime);
                }
                else
                {
                    if(timer + deathTime <Time.time )
                    {
                        launcher.MoveArrowUsedPool(gameObject);
                    }
                    tr.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);

                }
            }

        } 


    }
}
