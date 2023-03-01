using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Pontaap.Studio
{
    public class LevelManager :Singleton<LevelManager>
    {
         public void LoadNextScene()
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 

         }
        public void RestartThisScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
        
    }
}
