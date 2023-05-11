using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // the explosion animation will call this function
    // at the end of the animation to destroy the ExplosionGO
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
