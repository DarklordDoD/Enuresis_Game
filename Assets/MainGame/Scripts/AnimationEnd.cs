using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    public void muveAgain()
    {
        GetComponentInParent<muvePet>().onTask = false;       
    }
}
