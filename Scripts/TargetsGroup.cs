using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsGroup : MonoBehaviour
{
    private Target[] _targets;

    private void Start()
    {
        _targets = GetComponentsInChildren<Target>();
    }

    public void ShowRandomTarget()
    {
        _targets[Random.Range(0, _targets.Length)].Show(); // спавним рамдомную цель в волне
    }
}
