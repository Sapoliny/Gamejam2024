using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectButton : MonoBehaviour
{
    public void Press()
    {
        transform.parent.parent.GetChild(0).gameObject.SetActive(false);
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
    }
}
