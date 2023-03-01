using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class MaterialAnim : MonoBehaviour
    {
        private Material m_mat;
        private Coroutine coroutine;

        [HideInInspector]
        public Vector4 centerUV;

        private void Start()
        {
            centerUV = new Vector4();
            centerUV.z = Screen.height;
            centerUV.w = Screen.width;
            m_mat = GetComponent<MeshRenderer>().sharedMaterial;
            m_mat.SetFloat("_Radius", 0);
        }

        /// <summary>
        /// Material Animasyonunu kontrol etmemize yarayan corotine.
        /// </summary>
        public void HandleMaterialAnim()
        {
            if(coroutine == null)
            {
                StopCoroutine(AnimMat());
            }

            coroutine = StartCoroutine(AnimMat());
        }
        private IEnumerator AnimMat()
        {
             m_mat.SetVector("_CenterOfUV", centerUV);
            while (true)
            {
                m_mat.SetFloat("_Radius", Mathf.Lerp(m_mat.GetFloat("_Radius"), 6, Time.deltaTime ));

                if (m_mat.GetFloat("_Radius") > 3)
                {
                    m_mat.SetFloat("_Radius", 0);
                    break;
                } 

                yield return null;
            }
        }
    }
}
