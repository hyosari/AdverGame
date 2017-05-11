using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class OXDescription : MonoBehaviour
{
    public void LoadNextLevel() {
        StartCoroutine(LevelLoad());
      
        SceneManager.LoadScene("Main");
    }
   IEnumerator LevelLoad() {
        yield return new WaitForSeconds(15f);
        
    }
 
} 