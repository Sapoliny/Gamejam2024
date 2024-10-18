using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackButton : MonoBehaviour
{
    public void Press()
    {
        transform.parent.parent.parent.parent.GetChild(0).gameObject.SetActive(true);
        transform.parent.parent.parent.parent.GetChild(1).gameObject.SetActive(false);
    }
}
