// SortingAlgorithms.cs
using System.Collections;
using UnityEngine;

public class SortingAlgorithms : MonoBehaviour
{
    private SortingManager sortingManager;
    [SerializeField] private float sortingDelay = 0.1f;

    void Start()
    {
        sortingManager = GetComponent<SortingManager>();
    }

    // 버블 정렬
    public IEnumerator BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // 교환
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;

                    sortingManager.UpdateVisual();
                    yield return new WaitForSeconds(sortingDelay);
                }
            }
        }
    }

    // 선택 정렬
    public IEnumerator SelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIdx = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIdx])
                    minIdx = j;
            }

            // 교환
            int temp = arr[minIdx];
            arr[minIdx] = arr[i];
            arr[i] = temp;

            sortingManager.UpdateVisual();
            yield return new WaitForSeconds(sortingDelay);
        }
    }

    // 삽입 정렬
    public IEnumerator InsertionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;

                sortingManager.UpdateVisual();
                yield return new WaitForSeconds(sortingDelay);
            }
            arr[j + 1] = key;
        }
    }

    // 퀵 정렬
    public IEnumerator QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            yield return StartCoroutine(Partition(arr, low, high));

            //yield return StartCoroutine(QuickSort(arr, low, pi - 1));
            //yield return StartCoroutine(QuickSort(arr, pi + 1, high));
        }
    }

    private IEnumerator Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;

                sortingManager.UpdateVisual();
                yield return new WaitForSeconds(sortingDelay);
            }
        }

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        sortingManager.UpdateVisual();
        yield return new WaitForSeconds(sortingDelay);

        yield return i + 1;
    }
}
