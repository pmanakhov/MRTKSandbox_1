// Copyright (c) Microsoft

using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

namespace MRTKSandbox
{
    [RequireComponent(typeof(Renderer))]
    public class ColorTap : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
    {
        [Header("Colors For Each State")]
        [SerializeField]
        private Color idle = Color.white;
        [SerializeField]
        private Color hovered = Color.cyan;
        [SerializeField]
        private Color selected = Color.blue;

        private Material material;

        private void Awake()
        {
            //PointerUtils.SetGazePointerBehavior(PointerBehavior.AlwaysOff);
        }

        private void Start()
        {
            if ((material = GetComponent<Renderer>().material) == null)
            {
                Debug.LogError(this.GetType().Name + ": Couldn't find a material. Disabling the script");
                enabled = false;
                return;
            }
        }

        public virtual void OnFocusEnter(FocusEventData eventData)
        {
            material.color = hovered;

            Debug.Log("OnFocusEnter");
        }

        public virtual void OnFocusExit(FocusEventData eventData)
        {
            material.color = idle;

            Debug.Log("OnFocusExit");
        }

        public virtual void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            material.color = selected;

            Debug.Log("OnPointerDown");
        }

        public virtual void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
        }

        public virtual void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerDragged");
        }

        public virtual void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            Debug.Log("OnPointerClicked");
        }
    }
}
