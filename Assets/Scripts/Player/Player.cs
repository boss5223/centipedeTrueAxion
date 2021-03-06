using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Player
{
    IEnumerator Move(Vector3 direction);
    IEnumerator Fire();

    IEnumerator Dead();
}
