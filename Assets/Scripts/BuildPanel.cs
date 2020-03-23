using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    public static BuildPanel instance;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

}
