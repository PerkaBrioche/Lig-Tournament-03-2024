using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Febucci;
using Febucci.UI.Effects;
using UnityEngine.UI;
using Unity.VisualScripting;

public class movement : MonoBehaviour
{
    public int INT_AvoidR;
    public int INT_AvoidL;
    public bool ObstacleAvoiding;
    public GameObject player;
    BoxCollider2D zone_slow;

    
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
    public Rigidbody2D player_body;
    
    
    public float vitesseDeplacement;
    public float InitvitesseDeplacement;
    public TextMeshPro LetterAction;
    public TextMeshPro LetterAvoidR;
    public TextMeshPro LetterAvoidL;
    public TextMeshProUGUI StreakAction;
    public int Streak;
    public List<KeyCode> LetterActionList;
    public List<KeyCode> esquiv_haut;
    public List<KeyCode> esquiv_bas;
    public bool Is_Player1Playing;
    public bool Is_Player2Playing;
    public int ActionState;
    private float timer = 0;
    private void Start()
    {
        Top.sprite = SpritesTop[4];
        RightLeg.sprite = SpritesLegs[4];
        LeftLeg.sprite = SpritesLegs[4];
        
    }

    void Update()
    {
        if (!ObstacleAvoiding && player.transform.position.y < -1)
        {
            INT_AvoidL = 0;
            INT_AvoidR = 0;
            LetterAvoidL.text = "";
            LetterAvoidR.text = "";
            Top.sprite = SpritesTop[ActionState];
            if (Is_Player1Playing)
            {
                RightLeg.sprite = SpritesLegs[ActionState];
                LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];
            }
            else if (Is_Player2Playing)
            {
                RightLeg.sprite = SpritesLegs[ActionState];
                LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];

            }
            vitesseDeplacement = InitvitesseDeplacement + (Streak / 2);
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
                    }
                    else if (ActionState == 6)
                    {
                        Is_Player2Playing = false;
                        Is_Player1Playing = true;
                        Vector3 newPosition = transform.position + new Vector3(vitesseDeplacement, 0f, 0f);
                        transform.position = newPosition;
                        Streak++;
                    }
                }
                else if (Input.anyKeyDown && !Input.GetKeyDown(touche))
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
        else
        {
            LetterAvoidL.text = esquiv_haut[0].ToString();
            LetterAvoidR.text = esquiv_haut[1].ToString();
            LetterAction.text = "";
            print("Yipi");
            if (obst.tag_o == "high")
            {
                while (timer < 4f)
                {
                    print("YIPIXX");
                    timer += Time.deltaTime;
                }
                Debug.Log("perdu !");

            }

            if (obst.tag_o == "low")
            {
                Debug.Log("Mais frr !");
                    while (timer < 4f)
                    {

                        if (Input.GetKeyDown(esquiv_bas[0]))
                        {
                            INT_AvoidR++;
                            Debug.Log("DROITE");
                        }
                        if (Input.GetKeyDown(esquiv_bas[1]))
                        {
                            INT_AvoidL++;
                            Debug.Log("GAUCHE");
                        }       
                        if (INT_AvoidL/150 > 3 && INT_AvoidR/150 > 3)
                        {
                        Debug.Log("YOOO");
                        timer = 1;
                        while (timer < 3)
                        { player_body.velocity = new Vector2(0, 7); timer += Time.deltaTime; }
                            
                            INT_AvoidL = 0;
                            INT_AvoidR = 0;
                        ObstacleAvoiding = false;
                         }
                        timer += Time.deltaTime;
                    }
                
                Debug.Log("perdu !");
                
                timer = 0;
             
            }
        }
        
    }
}

