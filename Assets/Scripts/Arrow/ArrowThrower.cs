using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 namespace Pontaap.Studio
{
    public class ArrowThrower : MonoBehaviour
    {
        private ScreenHandler screenHandler;
        public int poolSize;
        public float waitNewArrowTime;

        private GameObject usablePool;
        private GameObject usedPool;
        private GameObject readyArrow;
        private Rigidbody readyArrowRb;
        private WaitForSeconds wait;
        private ArrowSO arrowSO;
 
        



        /// <summary>
        /// Oklarýn obje havuzunda oluþturulmasý iþlemi.
        /// </summary>
        void Init()
        {
 
            arrowSO = GameManager.GetInstance.arrowSO;
            usablePool = new GameObject("Usable Pool");
            usedPool = new GameObject("Used Pool");
            usablePool.transform.SetParent(transform, false);
            usedPool.transform.SetParent(transform, false);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject arrow = Instantiate(arrowSO.arrowPrefab, usablePool.transform);
                arrow.transform.localPosition = Vector3.zero;
                arrow.SetActive(false);
            }

            screenHandler = FindObjectOfType<ScreenHandler>();

            //Action members
            EventManager.AFire += Fire;

            //Function Init
            SetArrow();
            wait = new WaitForSeconds(waitNewArrowTime);
            GameManager.GetInstance.UpdateArrowCount(poolSize,true);


        }

        private void Start()
        {
            Init();
        }

        public void SpawnNewArrows(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject arrow = Instantiate(arrowSO.arrowPrefab, usablePool.transform);
                arrow.transform.localPosition = Vector3.zero;
                arrow.SetActive(false);
                poolSize++;
            }
            GameManager.GetInstance.UpdateArrowCount(amount, true);
        }

        public GameObject SetGetArrow
        {
            set { readyArrow = value; }
            get { return readyArrow; }
        }
        void SetArrow()
        {
            if (usablePool.transform.childCount > 0 )
            {
                GameObject arrow = usablePool.transform.GetChild(0).gameObject;
                if (arrow)
                {
                    arrow.SetActive(true);
                    SetGetArrow = arrow;
                    readyArrowRb = arrow.GetComponent<Rigidbody>();
 

                }
            }

        }

        /// <summary>
        /// Kullanýlýþ oklarý belirtilen süre içinde disable hale getirir.
        /// </summary>
        public void MoveArrowUsedPool(GameObject arrow)
        {
            arrow.SetActive(false);
            arrow.transform.localPosition = Vector3.zero;
            screenHandler.WorldPos = Vector3.zero;
        }

        /// <summary>
        /// Okun ateþlenmesini saðlar.
        /// </summary>
        void Fire()
        {

            if (screenHandler.WorldPos != Vector3.zero && SetGetArrow && poolSize > 0 && GameManager.GetInstance.gameState == GameState.Play)
            {
              
                poolSize--;
                Transform arrow = SetGetArrow.transform;
                arrow.GetComponent<AudioSource>().Play();
                arrow.SetParent(usedPool.transform);
                arrow.GetComponent<Arrow>().OpenTrails();
                Vector3 targetPos = screenHandler.WorldPos;
                Vector3 dir = (targetPos - arrow.position).normalized;
                arrow.rotation = Quaternion.LookRotation(dir);
                readyArrowRb.AddForce(dir * arrowSO.throwForce, ForceMode.Impulse);
                arrow.GetComponent<Arrow>().isThrowed = true;
                arrow.GetComponent<Arrow>().timer = Time.time;
                 GameManager.GetInstance.UpdateArrowCount();
                StartCoroutine(SetNewArrow());
               

            }


        }

        IEnumerator SetNewArrow()
        {
            yield return wait;

            SetArrow();
        }
    }



}
