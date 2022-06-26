using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SillySecrets
{
    public enum SwipeType { None, Up, Down, Right, Left }

    public class TouchControl : MonoBehaviour
    {

        private static TouchControl instance;
        public static TouchControl Instance  // Not Persistent Instance.
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TouchControl>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(TouchControl).Name;
                        instance = obj.AddComponent<TouchControl>();
                    }
                }
                return instance;
            }
        }

        private bool isDraging = false;
        private Vector2 startTouch, swipeDelta;

        public Vector2 SwipeDelta { get { return swipeDelta; } }

        public static SwipeType swipeType;

        private void Update()
        {
            #region Standalone Inputs
            if (Input.GetMouseButtonDown(0))
            {
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                Reset();
                swipeType = SwipeType.None;
            }
            #endregion

            // Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {

                if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            //Did we cross the distance?
            if (swipeDelta.magnitude > 50)
            {
                //Which direction?
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or right
                    if (x < 0)
                        swipeType = SwipeType.Left;
                    else
                        swipeType = SwipeType.Right;
                }
                else
                {
                    // Up or down
                    if (y < 0)
                        swipeType = SwipeType.Down;
                    else
                        swipeType = SwipeType.Up;
                }
                Reset();
            }
        }
        void Reset()
        {
            startTouch = swipeDelta = Vector2.zero;
            isDraging = false;
        }
    }
}

