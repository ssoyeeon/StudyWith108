// SortingManager.cs
using UnityEngine;

public class SortingManager : MonoBehaviour
{
    [SerializeField] private int arraySize = 50;
    [SerializeField] private float maxHeight = 10f;
    [SerializeField] private float barWidth = 0.5f;
    [SerializeField] private Material barMaterial;

    private GameObject[] bars;
    private int[] values;

    void Start()
    {
        InitializeArray();
        CreateVisualBars();
    }

    private void InitializeArray()
    {
        values = new int[arraySize];
        bars = new GameObject[arraySize];

        // ·£´ý °ª »ý¼º
        for (int i = 0; i < arraySize; i++)
        {
            values[i] = Random.Range(1, (int)maxHeight);
        }
    }

    private void CreateVisualBars()
    {
        float startX = -(arraySize * barWidth) / 2;

        for (int i = 0; i < arraySize; i++)
        {
            GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bar.transform.parent = transform;

            float height = values[i];
            bar.transform.localScale = new Vector3(barWidth, height, 1);
            bar.transform.position = new Vector3(startX + (i * barWidth), height / 2, 0);

            bar.GetComponent<MeshRenderer>().material = barMaterial;
            bars[i] = bar;
        }
    }

    public void UpdateVisual()
    {
        for (int i = 0; i < arraySize; i++)
        {
            float height = values[i];
            bars[i].transform.localScale = new Vector3(barWidth, height, 1);
            bars[i].transform.position = new Vector3(
                bars[i].transform.position.x,
                height / 2,
                0
            );
        }
    }
}
