using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    [SerializeField] buttonSelection buttonSelection;
    [SerializeField] int thisIndex;

    public Sprite highlightedButton;
    public Sprite normalButton;
    Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
       buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonSelection.index == thisIndex)
        {
            buttonImage.sprite = highlightedButton;
        }
        else
        {
            buttonImage.sprite = normalButton;
        }
    }
}
