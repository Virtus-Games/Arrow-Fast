using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Pontaap.Studio
{
    public class CircleThrower : MonoBehaviour
    {

        public int poolSize;
        public GameObject circlePrefab;

        public int maxThrowForce;
        public int minThrowForce;
 
        public int maxSpawnDuration;
        public int minSpawnDuration;
        private GameObject pool;
        private GameObject usedPool;
        private Transform throwPoint;
        GameObject circle;
        void Init()
        {
            throwPoint = transform.GetChild(0);
            


            pool = new GameObject("Circle Pool");
            usedPool = new GameObject("Used Pool");
            usedPool.transform.SetParent(transform);
            pool.transform.SetParent(transform);
            pool.transform.localPosition = Vector3.zero;

            for (int i = 0; i < poolSize; i++)
            {
                GameObject circleObj = Instantiate(circlePrefab, pool.transform, false);
                circleObj.SetActive(false);
            }


            StartCoroutine(ThrowCircleTimer());
            GameManager.GetInstance.UpdateCircleCount(poolSize,true);

        }

        private void Start()
        {
            Init();
        }

        void UseCircle()
        {
            if ( poolSize > 0 && GameManager.GetInstance.gameState == GameState.Play)
            {
                poolSize--;
                int randomForce = Random.Range(minThrowForce, maxThrowForce);
                circle = pool.transform.GetChild(0).gameObject;
                circle.GetComponent<Rigidbody>();
                circle.SetActive(true);
                circle.GetComponent<Rigidbody>().AddForce(throwPoint.forward * randomForce, ForceMode.Impulse);
                circle.transform.SetParent(usedPool.transform);
                GameManager.GetInstance.UpdateCircleCount();
 

            }
             
             
        }
        /// <summary>
        /// Kullan�lm�� olan circle '� i�i bittikten sonra disable yapar.
        /// </summary>
        /// <param name="usedCircle">Kullan�lm�� circle.</param>
        public void MoveCircleUsedPool(GameObject usedCircle)
        {
            if (poolSize == 0 && GameManager.GetInstance.gameState != GameState.Win)
                GameManager.GetInstance.SetGameState(GameState.Lose);   
            usedCircle.SetActive(false);

        }

        /// <summary>
        /// Reklamlar izlendi�inde y�kseltme �d�l� al�nd�ysa �al��t�r.
        /// </summary>
        /// <param name="value">Her bir circle thrower objesinde ayarlanacak olan circle miktar�.</param>
        public void SpawnNewCircles(int value = 1)
        {
            for (int i = 0; i < value; i++)
            {
                GameObject circle = Instantiate(circlePrefab, pool.transform, false);
                circle.SetActive(false);
                poolSize++;
            }
            GameManager.GetInstance.UpdateCircleCount(value, true);
        }

        IEnumerator ThrowCircleTimer()
        {
            while (true)
            {
                int randormTime = Random.Range(minSpawnDuration, maxSpawnDuration);

                ; yield return new WaitForSeconds(randormTime);

                UseCircle();



            }
        }
    }
}
