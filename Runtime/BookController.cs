using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Book
{
    internal class BookController : MonoBehaviour
    {
        [SerializeField] float pageTurnSpeed = 0.5f;
        [SerializeField] List<Transform> pages;
        [SerializeField] int index = -1;

        private void Start()
        {
            InitialState();
        }

        public void InitialState()
        {
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].transform.rotation = Quaternion.identity;
            }
            pages[0].SetAsLastSibling();

        }

        public void OnRotateForward(InputValue value)
        {
            if (!value.isPressed || index == pages.Count - 1) { return; }
            index++;
            float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
            pages[index].SetAsLastSibling();
            StartCoroutine(Rotate(pages[index], angle, true));

        }

        public void OnRotateBackward(InputValue value)
        {
            if (!value.isPressed || index == -1) { return; }
            float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
            pages[index].SetAsLastSibling();
            StartCoroutine(Rotate(pages[index], angle, false));
            index--;
        }

        

        IEnumerator Rotate(Transform page, float angle, bool forward)
        {
            float value = 0f;
            while (true)
            {
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
                value += Time.deltaTime * pageTurnSpeed;
                page.rotation = Quaternion.Slerp(page.rotation, targetRotation, value); //smoothly turn the page
                float angleRemains = Quaternion.Angle(page.rotation, targetRotation); //calculate the angle between the given angle of rotation and the current angle of rotation
                if (angleRemains < 0.1f)
                {
                    break;
                }
                yield return null;

            }
        }





    }
}
