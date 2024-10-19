using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackButton : MonoBehaviour
{
    public void Press()
    {
        Transform grandgrandpa = transform.parent.parent.parent;
        for (int i = 0; i < grandgrandpa.childCount; i++)
        {
            grandgrandpa.GetChild(i).gameObject.SetActive(true);
        }

        Transform grandpa = transform.parent.parent;

        grandpa.GetChild(0).gameObject.SetActive(true);
        grandpa.GetChild(1).gameObject.SetActive(false);
    }
}
