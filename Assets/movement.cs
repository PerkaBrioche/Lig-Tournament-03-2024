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
    public SpriteRenderer player_sprite;
    public Sprite stun_sprite;
    public Sprite low_sprite;
    public List<Sprite> high_sprite;
    
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
    public TextMeshPro Warning;
    public TextMeshPro Chrono;
    public TextMeshPro bravo;
    public int chrono;
    public float chrono_f;
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
    public float timer_esq_bas = 0;
    public float timer_esq_haute = 0;
    public bool esq_basse = false;
    public bool esq_haute = false;
    public bool arrow_first = false;
    public bool s_first = false;
    private Vector3 shrink = new Vector2(0, 5);
    private Vector3 normal_height = new Vector2(0, -0.9f);
    private bool inversed = false;
    public float arrive;
    public bool termine = false;
    
    
    private void Start()
    {
        Top.sprite = SpritesTop[4];
        RightLeg.sprite = SpritesLegs[4];
        LeftLeg.sprite = SpritesLegs[4];
        Warning.text = "";
        Chrono.text = "0";
        bravo.text = "";
    }


    void Update()
    {   if (transform.position.x >= arrive) termine = true;

        if (!termine)
        {
            chrono_f += Time.deltaTime;
            chrono = (int)chrono_f;
            Chrono.text = chrono.ToString();

            if (Streak < 7 && inversed) { LetterActionList.Reverse(); inversed = false; Warning.text = ""; }
            if (Streak >= 7 && !inversed) { LetterActionList.Reverse(); inversed = true; Warning.text = ""; }

            if (Streak > 3 && !inversed) { Warning.text = "Warning ! " + (7 - Streak); }

            GetComponent<CapsuleCollider2D>().enabled = true;
            timer = 0;

            if (esq_basse) // en gros, l'idée est de lever la hitbox du perso et de slow_mo le temps que l'objet passe
            {
                player_body.constraints = RigidbodyConstraints2D.FreezePositionY;
                Debug.Log("fonction esquive appelée");
                stun = true;
                GetComponent<CapsuleCollider2D>().offset = shrink;
                zone_slow.GetComponent<BoxCollider2D>().offset = shrink;
                timer_esq_bas += Time.deltaTime;
                Debug.Log(timer_esq_bas);
                Debug.Log("taille normale");
                if (timer_esq_bas >= 1)
                {
                    timer_esq_bas = 0;
                    esq_basse = false; GetComponent<CapsuleCollider2D>().offset = normal_height;
                    zone_slow.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                }
            }
            if (esq_haute)
            {
                player_sprite.enabled = true;
                RightLeg.enabled = false;
                LeftLeg.enabled = false;
                Top.enabled = false;
                player_sprite.sprite = high_sprite[0];
                player_body.velocity = new Vector2(0, 2.5f);  //E : je donne une vélocité qui va vers le haut le temps que le perso passe au dessus de la grenade
                timer_esq_haute += Time.deltaTime;
                if (timer_esq_haute > 0.66f) player_sprite.sprite = high_sprite[2];
                else if (timer_esq_haute > 0.33) player_sprite.sprite = high_sprite[1];

                if (timer_esq_haute > 1)
                {
                    INT_AvoidL = 0; // E : après je reset les deux nombres
                    INT_AvoidR = 0;
                    ObstacleAvoiding = false;
                    esq_haute = false;
                    timer_esq_haute = 0;
                }
            }

            if (stun) // E : en gros l'idée c'est que pendant une seconde, tu peux plus bouger si t'as été touchée (en plus de la poussée qui était la de base), plus de 1 secondes, je trouve ca fait un peu bcp en vrai
            {
                player_sprite.enabled = true;
                RightLeg.enabled = false;
                LeftLeg.enabled = false;
                Top.enabled = false;
                if (esq_basse)
                { player_sprite.sprite = low_sprite; }
                else
                { player_sprite.sprite = stun_sprite; }
                Debug.Log("stun !");
                player_body.constraints = RigidbodyConstraints2D.FreezePositionX;
                timer_stun += Time.deltaTime;
                if (timer_stun > 1)
                {
                    stun = false;
                    timer_stun = 0;
                    RightLeg.enabled = true;
                    LeftLeg.enabled = true;
                    Top.enabled = true;
                    RightLeg.enabled = true;
                    LeftLeg.enabled = true;
                    player_sprite.enabled = false;
                }

            }

            else
            {

                if (!ObstacleAvoiding && player.transform.position.y < -1)
                {
                    player_sprite.enabled = false;
                    RightLeg.enabled = true;
                    LeftLeg.enabled = true;
                    Top.enabled = true;
                    s_first = false;
                    arrow_first = false;
                    INT_AvoidL = 0;
                    INT_AvoidR = 0;
                    LetterAvoidL.text = "";
                    LetterAvoidR.text = "";
                    Top.sprite = SpritesTop[ActionState % 6];
                    if (Is_Player1Playing)
                    {
                        RightLeg.sprite = SpritesLegs[ActionState % 6];
                        LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];
                    }
                    else if (Is_Player2Playing)
                    {
                        RightLeg.sprite = SpritesLegs[ActionState % 6];
                        LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];

                    }
                    vitesseDeplacement = InitvitesseDeplacement + (Streak / 2);
                    if (Is_Player1Playing || Is_Player2Playing)
                    {
                        LetterAction.text = LetterActionList[ActionState % 6].ToString();
                        StreakAction.text = "x" + Streak;
                    }
                    else
                    {
                        LetterAction.text = "";
                        StreakAction.text = "...";
                    }
                    if (Is_Player1Playing || Is_Player2Playing && ActionState < LetterActionList.Count)
                    {
                        KeyCode touche = LetterActionList[ActionState % 6];
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
                                    transform.position = newPosition; // globalement, si il y a assez d'esapce, je mets la position normale mais si après le déplacement, on est trop proche de l'objet, je me limite à une certaine distance de l'objet
                                else
                                    transform.position = zone_slow.transform.position - new Vector3(1, 0, 0);

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
                        if (!arrow_first && Input.GetKeyDown(esquiv_haut[1])) // en gros, je vois quelle touche a été pressée en premier, et j'attend que l'autre soit pressée
                        {
                            s_first = true;
                            Debug.Log("touche pressée");
                        }

                        if (!s_first && Input.GetKeyDown(esquiv_haut[0]))
                        {
                            arrow_first = true;
                            Debug.Log("touche pressée");
                        }

                        if (arrow_first)
                        {
                            while (timer < 4f)
                            {
                                timer += Time.deltaTime;
                                if (Input.GetKeyDown(esquiv_haut[1]))
                                {
                                    Debug.Log("Autre touche pressée");

                                    esq_basse = true; // ce booléen renvoie vers le fait de lever la hitbox du perso
                                }
                            }
                            timer = 0;
                        }

                        if (s_first)
                        {
                            while (timer < 4f)
                            {
                                timer += Time.deltaTime;
                                if (Input.GetKeyDown(esquiv_haut[0]))
                                {
                                    Debug.Log("Autre touche pressée");

                                    esq_basse = true;
                                }
                            }
                            timer = 0;
                        }


                    }

                    if (obst.tag_o == "low")
                    {
                        LetterAvoidL.text = esquiv_bas[0].ToString();
                        LetterAvoidR.text = esquiv_bas[1].ToString();
                        if (Input.GetKeyDown(esquiv_bas[0]))
                        {
                            INT_AvoidR++; // E: le principe c qu'il faut appuyer un nombre de fois sur flèche du haut / Z pour que le perso saute, avoidR et avoidL comptent le nombre de fois que ces touches sont appuyées
                        }
                        if (Input.GetKeyDown(esquiv_bas[1]))
                        {
                            INT_AvoidL++;
                        }
                        if (INT_AvoidL > 3 && INT_AvoidR > 3)
                        {
                            esq_haute = true;
                        }

                    }
                }

            }
        }
        else bravo.text = "BRAVO !!";
    }
}

