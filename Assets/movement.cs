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
    public GameObject zone_slow;

    
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
    public float timer = 0;
    public static bool stun = false;
    public float timer_stun = 0;
    
    
    
    private void Start()
    {
        Top.sprite = SpritesTop[4];
        RightLeg.sprite = SpritesLegs[4];
        LeftLeg.sprite = SpritesLegs[4];
        
    }

    void Update()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        timer = 0;
        if (stun) // E : en gros l'id�e c'est que pendant une seconde, tu peux plus bouger si t'as �t� touch�e (en plus de la pouss�e qui �tait la de base), plus de 1 secondes, je trouve ca fait un peu bcp en vrai
        {
            Debug.Log("stun !");
            player_body.constraints = RigidbodyConstraints2D.FreezePositionX;
            timer_stun += Time.deltaTime;
            if (timer_stun > 1)
            { stun = false; timer_stun = 0; }

        }

        else
        {

            if (!ObstacleAvoiding && player.transform.position.y < -1)
            {
                INT_AvoidL = 0;
                INT_AvoidR = 0;
                LetterAvoidL.text = "";
                LetterAvoidR.text = "";
                Top.sprite = SpritesTop[ActionState%6];
                if (Is_Player1Playing)
                {
                    RightLeg.sprite = SpritesLegs[ActionState%6];
                    LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];
                }
                else if (Is_Player2Playing)
                {
                    RightLeg.sprite = SpritesLegs[ActionState%6];
                    LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];

                }
                vitesseDeplacement = InitvitesseDeplacement + (Streak / 2);
                if (Is_Player1Playing || Is_Player2Playing)
                {
                    LetterAction.text = LetterActionList[ActionState%6].ToString();
                    StreakAction.text = "x" + Streak;
                }
                else
                {
                    LetterAction.text = "";
                    StreakAction.text = "...";
                }
                if (Is_Player1Playing || Is_Player2Playing && ActionState < LetterActionList.Count)
                {
                    KeyCode touche = LetterActionList[ActionState%6];
                    if (Input.GetKeyDown(touche))
                    {
                        //Debug.Log("Bonne lettre : " + touche);
                        ActionState++;
                        if (ActionState == 3)
                        {
                            Is_Player1Playing = false;
                            Is_Player2Playing = true;
                            Vector3 newPosition = transform.position + new Vector3(vitesseDeplacement, 0f, 0f);
                            if (seuil_pos.pos_x - newPosition.x > 2.1f)
                            transform.position = newPosition; // globalement, si il y a assez d'esapce, je mets la position normale mais si apr�s le d�placement, on est trop proche de l'objet, je me limite � une certaine distance de l'objet
                            else 
                                transform.position = zone_slow.transform.position - new Vector3(1,0,0);

                            Streak++;
                        }
                        else if (ActionState == 6)
                        {
                            Is_Player2Playing = false;
                            Is_Player1Playing = true;
                            Vector3 newPosition = transform.position + new Vector3(vitesseDeplacement, 0f, 0f);
                            if (seuil_pos.pos_x - newPosition.x > 2.1f)
                                transform.position = newPosition;
                            else
                                Debug.Log("feur");
                                transform.position = zone_slow.transform.position - new Vector3(1, 0, 0);
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
                
                LetterAction.text = "";
                print("Yipi");
                if (obst.tag_o == "high")
                {
                    ObstacleAvoiding = true;
                    LetterAvoidL.text = esquiv_haut[0].ToString();
                    LetterAvoidR.text = esquiv_haut[1].ToString();
                    if (Input.GetKeyDown(esquiv_haut[1]))
                    {
                        while (timer < 4f)
                        {
                            timer += Time.deltaTime;
                            if (Input.GetKeyDown(esquiv_haut[0]))
                            {
                                GetComponent<CircleCollider2D>().enabled = false; // bon l� je s�che
                            }
                        }
                        timer = 0;
                    }
                    if (Input.GetKeyDown(esquiv_haut[0]))
                    {
                        while (timer < 4f)
                        {
                            timer += Time.deltaTime;
                            if (Input.GetKeyDown(esquiv_haut[1]))
                            {
                                GetComponent<CapsuleCollider2D>().enabled = false;
                            }
                        }
                        timer = 0;
                    }

                    

                }

                if (obst.tag_o == "low")
                {
                    LetterAvoidL.text = esquiv_bas[0].ToString();
                    LetterAvoidR.text = esquiv_bas[1].ToString();
                    Debug.Log("Mais frr !");
                        if (Input.GetKeyDown(esquiv_bas[0]))
                        {
                        Debug.Log(INT_AvoidR);  
                        INT_AvoidR++; // E: le principe c qu'il faut appuyer un nombre de fois sur fl�che du haut / Z pour que le perso saute, avoidR et avoidL comptent le nombre de fois que ces touches sont appuy�es
                        }
                        if (Input.GetKeyDown(esquiv_bas[1]))
                        {
                        Debug.Log(INT_AvoidL);
                        INT_AvoidL++;
                        }
                        if (INT_AvoidL > 3 && INT_AvoidR > 3) 
                        {
                            timer = 1;
                            while (timer < 3)
                            { player_body.velocity = new Vector2(0, 7); timer += Time.deltaTime; } //E : je donne une v�locit� qui va vers le haut le temps que le perso passe au dessus de la grenade

                            INT_AvoidL = 0; // E : apr�s je reset les deux nombres
                            INT_AvoidR = 0;
                            ObstacleAvoiding = false;
                        }
                    

                    Debug.Log("perdu !");

                    timer = 0;

                }
            }

        }
    }
}

