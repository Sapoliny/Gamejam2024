using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectButton : MonoBehaviour
{
    public void Press()
    {
        Transform dad = transform.parent;
        Transform grandpa = transform.parent.parent;

        dad.GetChild(0).gameObject.SetActive(false);
        dad.GetChild(1).gameObject.SetActive(true);

        
        for (int i = 0; i < grandpa.childCount;i++)
        {
            if (grandpa.GetChild(i) != dad)
            {
                grandpa.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
