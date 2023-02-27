using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class KeepObjekt : MonoBehaviour
{
    public List<GameObject> objects;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        objects = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == this.gameObject.name).ToList();

        if (objects.Count > 1)
            Destroy(objects[1]);
    }
}
