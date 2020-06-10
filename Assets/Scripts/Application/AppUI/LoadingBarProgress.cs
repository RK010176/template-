using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App
{
    public class LoadingBarProgress : MonoBehaviour
    {

        [SerializeField]
        private FloatVal _loadingProcessValue;


        private RectTransform rt;

        void Awake()
        {
            rt = GetComponent(typeof(RectTransform)) as RectTransform;
        }


        void Update()
        {
            rt.sizeDelta = new Vector2(_loadingProcessValue.Value * 400, 20f);
        }
    }
}