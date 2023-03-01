using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pontaap.Studio
{
    public class ScreenHandler : MonoBehaviour
    {
        private Camera cam;
        private Vector3 worldPos;
        private Vector2 uvCord;
        /// <summary>
        /// Gönderilen ýþýn bir cube'e çarparsa bu çarpmanýn dünya kordinatlarý.
        /// </summary>
        public Vector3 WorldPos { get { return worldPos; } set { worldPos = value; } }
        /// <summary>
        /// Gönderilen ýþýn bir cube'e çarparsa carpýlan bu noktanýn uv kordinatlarý.
        /// </summary>
        public Vector2 UVCord { get { return uvCord; } }

        private void Awake()
        {
            cam = Camera.main;
            EventManager.AFire += HandleScreenToWorld;
         }

         
        void HandleScreenToWorld()
        {
            if(!cam)
            {
                cam = Camera.main;  
            }
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                worldPos = hit.point;
                uvCord = hit.textureCoord; 
                 
            } 
        }

       


    }
}
