using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FenceManager : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _timeBetweenFences = 10;
    [SerializeField] private float _currentTimeBetweenFences = 10;
    
    [SerializeField] private float _leftFencePositionX = -4.5f;
    [SerializeField] private float _rightFencePositionX = 0;

    public FenceGroup fenceGroupPrefab;
    public Fence fencePrefab;
    public Transform spawnFenceGroupPosition;
    
    private List<FenceGroup> _fenceGroups = new List<FenceGroup>();
    private List<FenceGroup> _fenceGroupInactivePool = new List<FenceGroup>();

    public int SUM;
    public int SUB;
    public int MUL;
    public int DIV;
    // Start is called before the first frame update
    void Start() {
        _fenceGroups = FindObjectsOfType<FenceGroup>().ToList();
    }

    private void FixedUpdate() {
        _currentTimeBetweenFences -= Time.deltaTime;
        if (_currentTimeBetweenFences<0) {
            SpawnFences();
            _currentTimeBetweenFences = _timeBetweenFences;
        }
        SetSpeedToFences(_speed);
    }

    private void SpawnFences() {
        // TODO: reutilize fences
        if (_fenceGroupInactivePool.Count != 0 && false) {
            //_fenceGroupInactivePool[0];
        }
        else {
            Debug.Log("Attempting to spawn a fence group");
            // instantiate fence group
            FenceGroup fenceGroup = Instantiate(fenceGroupPrefab, spawnFenceGroupPosition.position, Quaternion.identity);
            // add fence group to list
            _fenceGroups.Add(fenceGroup);
            
            // add fences 
            fenceGroup.AddFence(CreateRandomFence(_leftFencePositionX));
            fenceGroup.AddFence(CreateRandomFence(_rightFencePositionX,fenceGroup.fences));
            //define fences operation and amount
        }

    }

    private Fence CreateRandomFence(float positionOffsetX,List<Fence> differentFrom = null) { //define fences operation and amount

        int randomAmount = 0;
        int randomOperationIndex = Random.Range(0,101);
        int chosenOperationIndex =0;

        switch (randomOperationIndex) {
            case < 41:
                SUM++;
                randomAmount = Random.Range(1,10);
                chosenOperationIndex = (int)Operation.ADD;
                break;
            case < 71:
                SUB++;
                randomAmount = Random.Range(1,10);
                chosenOperationIndex = (int)Operation.SUB;
                break;
            case < 86:
                MUL++;
                randomAmount = Random.Range(2,5);
                chosenOperationIndex = (int)Operation.MUL;
                break;
            case < 101:
                DIV++;
                randomAmount = Random.Range(2,5);
                chosenOperationIndex = (int)Operation.DIV;
                break;
            
        }
        return Instantiate(fencePrefab,
            new Vector3(
                spawnFenceGroupPosition.position.x + positionOffsetX,
                spawnFenceGroupPosition.position.y,
                spawnFenceGroupPosition.position.z
            ),
            Quaternion.identity).SetOperation((Operation)chosenOperationIndex).SetAmount(randomAmount);
            
    }
    
    public void SetSpeedToFences(float speed) {
        _speed = speed;
        foreach (FenceGroup fenceGroup in _fenceGroups) {
            fenceGroup.SetSpeed(speed);
        }
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("collided");
        FenceGroup fenceGroup = other.gameObject.GetComponent<FenceGroup>();
        if (fenceGroup) {
            other.gameObject.SetActive(false);
            _fenceGroupInactivePool.Add(fenceGroup);
        }
        
    }
}
