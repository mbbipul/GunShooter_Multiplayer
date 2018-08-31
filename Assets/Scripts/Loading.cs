using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

    /*[SerializeField]
    private RectTransform rectComponent;
    */
    [SerializeField]
    private float rotateSpeed = 200f;

    private void Start()
    {
       // rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
       transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
