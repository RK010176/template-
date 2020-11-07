using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _foregroundImage;
        [SerializeField] private float _updateSpeedSeconds = 0.5f;

        private void Awake()
        {
            GetComponentInParent<PlayerController>().OnHealthPctChanged += HandleHealthChanged;
        }

        public void HandleHealthChanged(float pct)
        {            
            StartCoroutine(ChangeToPct(pct));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = _foregroundImage.fillAmount;
            float elapsed = 0f;
            while (elapsed < _updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                _foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / _updateSpeedSeconds);
                yield return null;
            }
            _foregroundImage.fillAmount = pct;
        }

        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}
