using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace App
{
    public class ScreenFader : MonoBehaviour
    {
        private CanvasGroup _cg;

        private void Awake()
        {
            if (GetComponent<CanvasGroup>() == null)
                gameObject.AddComponent<CanvasGroup>();

            _cg = GetComponent<CanvasGroup>();
        }

        public void FadeToBlack()
        {
            StartCoroutine(FadeOut());
        }
        public void FadeToFullOpacity()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {

            _cg.alpha = 0;              
            yield return new WaitForSeconds(1);
        }
        private IEnumerator FadeOut()
        {

            _cg.alpha = 1;
            yield return new WaitForSeconds(1);
        }

    }
}