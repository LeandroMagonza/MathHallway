using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Fence : MonoBehaviour {
    public Operation operation;
    public int amount;

    public new Collider collider;
    public TextMeshProUGUI amountDisplay;
    public MeshRenderer fenceOperationMaterial;
    public Material[] fenceMaterials;
    public Fence SetOperation(Operation operation) {
        this.operation = operation;
        fenceOperationMaterial.material = fenceMaterials[(int)operation];
        return this;
    }
    public Fence SetAmount(int amount) {
        this.amount = amount;
        amountDisplay.text = OperationText.GetOperationText(operation)+amount;
        amount.ToString();
        return this;
    }

    public void DisableCollider() {
        collider.enabled = false;
    }
    public void EnableCollider() {
        collider.enabled = true;
    }
}

public enum Operation {
    ADD ,
    SUB,
    MUL,
    DIV
}
public static class OperationText {
    public static Dictionary<Operation, string> texts = new Dictionary<Operation, string>
    {
        { Operation.ADD , "+" },
        { Operation.SUB , "-" },
        { Operation.MUL , "X" },
        { Operation.DIV , "%" }
    };
    public static string GetOperationText(Operation operation) {
        if (texts.ContainsKey(operation)) {
            return texts[operation];
        }
        else {
            return "OPERATION_NOT_FOUND";
        }
    }
    
}