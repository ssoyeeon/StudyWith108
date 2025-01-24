// UIManager.cs
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private SortingAlgorithms sortingAlgorithms;
    private SortingManager sortingManager;

    void Start()
    {
        sortingAlgorithms = FindObjectOfType<SortingAlgorithms>();
        sortingManager = FindObjectOfType<SortingManager>();
    }

    public void StartBubbleSort()
    {
        StartCoroutine(sortingAlgorithms.BubbleSort(sortingManager.GetValues()));
    }

    public void StartSelectionSort()
    {
        StartCoroutine(sortingAlgorithms.SelectionSort(sortingManager.GetValues()));
    }

    public void StartInsertionSort()
    {
        StartCoroutine(sortingAlgorithms.InsertionSort(sortingManager.GetValues()));
    }

    public void StartQuickSort()
    {
        int[] arr = sortingManager.GetValues();
        StartCoroutine(sortingAlgorithms.QuickSort(arr, 0, arr.Length - 1));
    }

    public void ResetArray()
    {
        sortingManager.ResetArray();
    }
}