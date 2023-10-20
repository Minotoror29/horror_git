using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;

    public void Initialize(int savedKids)
    {
        title.text = "You managed to escape the forest\r\nand save " + savedKids.ToString() + " kids !";
    }
}
