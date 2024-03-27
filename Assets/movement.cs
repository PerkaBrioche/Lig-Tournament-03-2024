using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Febucci;
using Febucci.UI.Effects;
using UnityEngine.UI;

public class movement : MonoBehaviour
{

    public List<Sprite> SpritesLegs;
    public List<Sprite> SpritesTop;
    public SpriteRenderer RightLeg;
    public SpriteRenderer LeftLeg;
    public SpriteRenderer Top;
    
    public SlideBehavior SlideBehavior;

    public int minValue;
    public int maxValue;
    public Color minColor;
    public Color maxColor;
    
    
    public float vitesseDeplacement;
    public float InitvitesseDeplacement;
    public TextMeshPro LetterAction;
    public TextMeshProUGUI StreakAction;
    public int Streak;
    public List<KeyCode> LetterActionList;
    public bool Is_Player1Playing;
    public bool Is_Player2Playing;
    public int ActionState;
    void Update()
    {
        Top.sprite = SpritesTop[ActionState];
        if (Is_Player1Playing)
        {
            RightLeg.sprite = SpritesLegs[ActionState];
            LeftLeg.sprite = SpritesLegs[0];
        }
        else if(Is_Player2Playing)
        {
            RightLeg.sprite = SpritesLegs[0];
            LeftLeg.sprite = SpritesLegs[ActionState];

        }
        vitesseDeplacement = InitvitesseDeplacement + (Streak/2);
        if (Is_Player1Playing || Is_Player2Playing)
        {
            LetterAction.text = LetterActionList[ActionState].ToString();
            StreakAction.text = "x" + Streak;
        }
        else
        {
            LetterAction.text = "";
            StreakAction.text = "...";
        }
        if (Is_Player1Playing || Is_Player2Playing && ActionState < LetterActionList.Count)
        {
            //Debug.Log("Wait For Action");
            
            KeyCode touche = LetterActionList[ActionState];
            
            if (Input.GetKeyDown(touche))
            {
                //Debug.Log("Bonne lettre : " + touche);
                ActionState++;
                if (ActionState == 3)
                {
                    Is_Player1Playing = false;
                    Is_Player2Playing = true;
                    Vector3 newPosition = transform.position + new Vector3(vitesseDeplacement, 0f, 0f);
                    transform.position = newPosition;
                    
                    Streak++;
                }else if (ActionState == 6)
                {
                    Is_Player2Playing = false;
                    Is_Player1Playing = true;
                    Vector3 newPosition = transform.position + new Vector3(vitesseDeplacement, 0f, 0f);
                    transform.position = newPosition;
                    Streak++;
                }
            }else if(Input.anyKeyDown && !Input.GetKeyDown(touche))
            {
                Streak = 0;
                if (Is_Player1Playing)
                {
                    ActionState = 0;
                }
                if (Is_Player2Playing)
                {
                    ActionState = 3;
                }
            }



            if (ActionState == LetterActionList.Count)
            {
                ActionState = 0;
            }
            
            SlideBehavior.baseFrequency = 5 + Streak;

            if (Streak > 0)
            {
                float t = Mathf.InverseLerp(minValue, maxValue, Streak);
                Color lerpedColor = Color.Lerp(minColor, maxColor, t);
                StreakAction.color = lerpedColor;
            }
            
        }
    }
}

