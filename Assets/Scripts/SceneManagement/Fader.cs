using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagment { 
public class Fader : MonoBehaviour
{
 CanvasGroup canvasGroup;
    void Start()
    {
     canvasGroup = GetComponent<CanvasGroup>();   
    }
       public IEnumerator FadeOut(float time) {
            var deltaAlpha = Time.deltaTime / time;
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += deltaAlpha;
                yield return null;

            }
        }
      public  IEnumerator FadeIn(float time)
        {
            var deltaAlpha = Time.deltaTime / time;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= deltaAlpha;
                yield return null;

            }
        }
        public void FadeInImmediate() {
            canvasGroup.alpha = 0;
        }
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
    }
}
