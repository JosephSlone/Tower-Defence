using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[SelectionBase]
public class BlockEditor : MonoBehaviour {

    int gridSize = 10;

    // Use this for initialization
    void Update()
    {
        SnapToGrid();
//        UpdateLabel();
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3(
            GetGridPos().x * gridSize,
            0f,
            GetGridPos().y * gridSize);
    }

    //private void UpdateLabel()
    //{
    //    TextMesh textMesh = GetComponentInChildren<TextMesh>();
    //    string labelText =
    //        GetGridPos().x +
    //        "," +
    //        GetGridPos().y;
    //    textMesh.text = labelText;
    //    gameObject.name = "Block " + labelText;
    //}

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }
}
