using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _currentTimeText;

    [SerializeField]
    private TextMeshProUGUI _currentScoreText;

    private void Update()
    {
        _currentScoreText.text = GlobalVariables.Instance.CompletedOrders.ToString();

        _currentTimeText.text = ((int)GlobalVariables.Instance.CurrentTime).ToString();
    }
}
