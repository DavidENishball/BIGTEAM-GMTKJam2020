using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Image_SynchronizeWithTimer : MonoBehaviour
{
    GameManager manager;
    public Image TheImage;

    public float MaximumTime;
    public float MaximumSize = 28.3f;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        TheImage = GetComponent<Image>();
        MaximumSize = TheImage.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        State_PlanPlayerMoves CurrentState = manager.stateMachine.GetState() as State_PlanPlayerMoves;

        if (CurrentState != null)
        {
            MaximumTime = CurrentState.owner.PlanningTimeForPlayer;
            float percentage = CurrentState.TimeRemaining / MaximumTime;
            float inversePercentage = 1.0f - percentage;
            TheImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inversePercentage * MaximumSize);
        }
        else
        {

            TheImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        }
    }
}
