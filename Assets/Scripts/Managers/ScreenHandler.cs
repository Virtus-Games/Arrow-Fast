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
        /// G�nderilen ���n bir cube'e �arparsa bu �arpman�n d�nya kordinatlar�.
        /// </summary>
        public Vector3 WorldPos { get { return worldPos; } set { worldPos = value; } }
        /// <summary>
        /// G�nderilen ���n bir cube'e �arparsa carp�lan bu noktan�n uv kordinatlar�.
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
