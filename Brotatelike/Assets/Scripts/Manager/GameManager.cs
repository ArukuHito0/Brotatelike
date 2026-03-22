using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ResultValueContainer resultContainer;

    private void Awake()
    {
        resultContainer.ResetData();
    }
}
