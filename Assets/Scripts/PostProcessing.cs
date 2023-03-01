using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class PostProcessing : MonoBehaviour
    {
        public Material postEffectMat;
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
             Graphics.Blit(source, destination, postEffectMat);
        }
    }
}
