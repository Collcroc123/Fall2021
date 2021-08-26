using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntResetter : MonoBehaviour
{
    public IntData totalRooms;

    private void Start()
    {
        totalRooms.value = 0;
    }
}
