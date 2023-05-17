using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeUIScript : MonoBehaviour
{   
    [SerializeField] private GameObject ellipseLeft;
    [SerializeField] private GameObject ellipseRight;

    [SerializeField] private GameObject leftCard;
    [SerializeField] private GameObject rightCard;

    // Update is called once per frame
    void Start()
    {   
        ellipseLeft.transform.position = new Vector3(leftCard.transform.position.x, 0, 0);
        ellipseRight.transform.position = new Vector3(rightCard.transform.position.x, 0, 0);
    }
}
