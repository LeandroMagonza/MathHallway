using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update
    public float boundSize = 3.5f;
    public int _number;
    public TextMeshProUGUI numberDisplay;
    public List<int> missions = new List<int>();
    public TextMeshProUGUI missionDisplay;
    [FormerlySerializedAs("collider")] public Collider playerCollider;
    void Start() {
        Cursor.visible = false;
        playerCollider = GetComponent<Collider>();
        Cursor.lockState = CursorLockMode.Confined;
        missions.Add(4);
        missions.Add(9);
        missions.Add(16);
        missions.Add(25);
        missions.Add(36);
        missions.Add(49);
        missions.Add(64);
        missions.Add(81);
        missions.Add(100);
        missionDisplay.text = missions[0].ToString();
        SetNumber(1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            playerCollider.enabled = false;
        }
        if (Input.GetMouseButtonUp(0)) {
            playerCollider.enabled = true;
        }
        float mouseRatioX = Input.mousePosition.x / Screen.width;
        float mouseRatioY = Input.mousePosition.y / Screen.height;

        
        Vector3 mousePos = new Vector3(
            (mouseRatioX * 2 - 1)*boundSize
            , transform.position.y, transform.position.z);
        transform.position = mousePos;
    }

    public void SetNumber(int number, bool checkMission = true) {
        _number = number;
        numberDisplay.text = number.ToString();
        if (checkMission) {
            CheckForMission(number);
        }

    }

    private void CheckForMission(int number) {
        if (number == missions.First()) {
            missions.Remove(number);
            Debug.Log("Resetting number");
            SetNumber(1,false);
            if (missions.Count == 0) {
                //enable message display
                Debug.Log("You won");
                Time.timeScale = 0;
                missionDisplay.text = "You Won!";
            }
            else {
                missionDisplay.text = missions[0].ToString();
            }
        }
    }

    // public void SetNumber(float floatNumber) {
    //     int number = Mathf.RoundToInt(floatNumber); 
    //     SetNumber(number);
    // }

    private void OnTriggerEnter(Collider other) {
        Fence fence = other.transform.parent.gameObject.GetComponent<Fence>();
        if (fence) {
            int newNumber = _number;
            switch (fence.operation) {
                case Operation.ADD:
                    newNumber += fence.amount ;
                    break;
                case Operation.SUB:
                    newNumber -= fence.amount;
                    break;
                case Operation.MUL:
                    newNumber *= fence.amount;
                    break;
                case Operation.DIV:
                    newNumber /= fence.amount;
                    break;
            }

            newNumber = Math.Clamp(newNumber, 1, 100);
            SetNumber(newNumber);
            fence.transform.parent.GetComponent<FenceGroup>().DisableFenceColliders();
        }
    }
}
