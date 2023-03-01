using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Pontaap.Studio
{
    public class MusicSetterButton : MonoBehaviour, IDragHandler
    {

        public RectTransform parent;
        public RectTransform current;
        public RectTransform fillBG;

        public TextMeshProUGUI volumePercent;
      
        private float maxX;
        private float minX;
        private Vector2 wantedPos;
        private int volume;
        private AudioSource backgroundMusic;
        private float volumeFloat;
        
        private void Start()
        {
           
            maxX = parent.rect.xMax;
            minX = parent.rect.xMin;
            backgroundMusic = GameManager.GetInstance.GetComponent<AudioSource>();
 
           
        }

        public void OnDrag(PointerEventData eventData)
        {
           
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, null, out wantedPos);

             wantedPos.y = 0;

            wantedPos.x = Mathf.Clamp(wantedPos.x, minX, maxX);
            current.anchoredPosition = wantedPos;
            volumeFloat = current.anchoredPosition.x / parent.rect.width;
            volumeFloat = Mathf.Clamp(volumeFloat, 0, 1);
            fillBG.sizeDelta = new Vector2(current.anchoredPosition.x, fillBG.rect.height);
             volume = (int)(volumeFloat * 100);
            volumePercent.SetText(volume.ToString());
           
            backgroundMusic.volume = volumeFloat;

        }

        private void OnDisable()
        {
            PlayerPrefs.SetInt("Volume", volume);
            PlayerPrefs.SetFloat("xpos", current.anchoredPosition.x); 
            PlayerPrefs.SetFloat("VolumeFloat", volumeFloat); 
        }

        private void OnEnable()
        {
              current.anchoredPosition = new Vector2(PlayerPrefs.GetFloat("xpos"),0);
            fillBG.sizeDelta = new Vector2(PlayerPrefs.GetFloat("xpos"), fillBG.rect.height);
            volumePercent.SetText(PlayerPrefs.GetInt("Volume").ToString());
            

            
        }






    }
}
