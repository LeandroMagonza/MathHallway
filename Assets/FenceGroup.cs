using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGroup : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    public List<Fence> fences = new List<Fence>();
    
    // Update is called once per frame
    void FixedUpdate() {
        transform.position = transform.position + new Vector3(0,0 , -_speed*Time.deltaTime);
    }

    public void OnEnable() {
        foreach (Fence fence in fences) {
            fence.EnableCollider();
        }
    }

    public void SetSpeed(float speed) {
        _speed = speed;
    }

    public void DisableFenceColliders() {
        foreach (Fence fence in fences) {
            fence.DisableCollider();
        }
    }

    public void AddFence(Fence fence) {
        fences.Add(fence);
        fence.transform.parent = transform;
    }
}
