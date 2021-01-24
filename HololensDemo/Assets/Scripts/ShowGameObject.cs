using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameObject : MonoBehaviour
{
    [Tooltip("GameObject to unhide")]
    [SerializeField]
    GameObject unhide = null;

    // Start is called before the first frame update
    void Start()
    {
        if (unhide != null)
        {
            unhide.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
