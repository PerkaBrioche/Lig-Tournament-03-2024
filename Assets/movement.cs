using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Febucci;
using Febucci.UI.Effects;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Unity.Burst.CompilerServices;

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
    public AudioSource audio_level;
    public AudioClip victory;
    public AudioSource effect;
    public AudioClip hit;
    public AudioClip ding_ding;
    public AudioClip sound_1;
    public AudioClip sound_2;
    public AudioClip sound_3;
    public AudioClip sound_fail;
    public TextMeshPro avancement;
    public bool avancement_moment = true;
    public int avancement_pos = 1;
    
    public SlideBehavior SlideBehavior;

    public int minValue;
    public int maxValue;
    public Rigidbody2D player_body;
    
    
    public float vitesseDeplacement;
    public float InitvitesseDeplacement;
    public TextMeshPro LetterAction;
    public TextMeshPro LetterAvoidR;
    public TextMeshPro LetterAvoidL;
    public TextMeshPro Warning;
    public TextMeshPro Chrono;
    public TextMeshPro bravo;
    public float chrono;
    public float chrono_f;
    public TextMeshProUGUI StreakAction;
    public int Streak;
    public List<KeyCode> LetterActionList;
    public List<string> CharActionList;
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
    public bool sound = true;
    public float timer_fade = 1;
    


    private void Start()
    {
        Top.sprite = SpritesTop[4];
        RightLeg.sprite = SpritesLegs[4];
        LeftLeg.sprite = SpritesLegs[4];
        Warning.text = "";
        Chrono.text = "0";
        bravo.text = "";
        audio_level.loop = true;
        audio_level.Play();
        avancement.text = " 0m / 150m ";
        audio_level.pitch = 1;
        effect.volume = 0.10f;
        audio_level.volume = 0.050f;
        LetterAvoidL.color =  new Color(0f, 1f, 0f);
        LetterAvoidR.color = new Color(0, 127f, 255f);
       // Warning.color = Color.black;
    }

    
    void Update()
    {   if (transform.position.x >= arrive) termine = true;

        if (!termine)
        {
            
                    if (Input.GetKeyDown(KeyCode.Escape)) termine = true;
            chrono_f += Time.deltaTime;
            chrono = Mathf.Round(chrono_f*100)/100;
            Chrono.text = chrono.ToString();

            if (Streak < 7 && inversed) { LetterActionList.Reverse(); CharActionList.Reverse(); inversed = false; Warning.text = ""; }
            if (Streak >= 7 && !inversed) { LetterActionList.Reverse(); CharActionList.Reverse(); inversed = true; Warning.text = ""; }

            if (Streak > 3 && !inversed) { Warning.text = "Inversion touches : " + (7 - Streak); }
            if (Streak <= 3 && !inversed) { Warning.text = ""; }

            GetComponent<CapsuleCollider2D>().enabled = true;
            timer = 0;

            if (esq_basse) // en gros, l'idée est de lever la hitbox du perso et de slow_mo le temps que l'objet passe
            {
                if (sound){ effect.volume = 1; effect.clip = ding_ding; effect.Play();  sound = false; effect.volume = 0.10f; }
                player_body.constraints = RigidbodyConstraints2D.FreezePositionY;
                stun = true;
                GetComponent<CapsuleCollider2D>().offset = shrink;
                zone_slow.GetComponent<BoxCollider2D>().offset = shrink;
                timer_esq_bas += Time.deltaTime;
                sound = false;
                if (timer_esq_bas >= 1)
                {
                    timer_esq_bas = 0;
                    esq_basse = false; GetComponent<CapsuleCollider2D>().offset = normal_height;
                    zone_slow.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                }
            }
            if (esq_haute)
            {
                if (sound) { effect.clip = ding_ding; effect.Play(); sound = false; }
                player_sprite.enabled = true;
                RightLeg.enabled = false;
                LeftLeg.enabled = false;
                Top.enabled = false;
                GetComponent<CapsuleCollider2D>().offset = shrink;
                player_sprite.sprite = high_sprite[0];
                player_body.velocity = new Vector2(0, 2.5f);  //E : je donne une vélocité qui va vers le haut le temps que le perso passe au dessus de la grenade
                timer_esq_haute += Time.deltaTime;
                if (timer_esq_haute > 0.66f) { player_sprite.sprite = high_sprite[2];}
                else if (timer_esq_haute > 0.33) player_sprite.sprite = high_sprite[1];

                if (timer_esq_haute > 1)
                {
                    INT_AvoidL = 0; // E : après je reset les deux nombres
                    INT_AvoidR = 0;
                    ObstacleAvoiding = false;
                    esq_haute = false;
                    timer_esq_haute = 0;
                    GetComponent<CapsuleCollider2D>().offset = normal_height;
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
                if (sound) { effect.clip = hit; effect.Play(); sound = false; }
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
                
                sound = true;
                if (!ObstacleAvoiding && player.transform.position.y < -1)
                {

                    if (((transform.position.x < 60 && transform.position.x > 40 && avancement_pos == 1)|| (transform.position.x > 90 && transform.position.x <110 && avancement_pos == 2)) && transform.position.x < 150) avancement_moment = true;

                    if (avancement_moment) 
                    {
                        avancement.text = (avancement_pos * 50).ToString() + "m / 150m";
                        timer_fade += Time.deltaTime;
                        Color lerpcol = Color.white;
                        
                        if (timer_fade > 3)
                        lerpcol.a = 1 / ((timer_fade - 3)* (timer_fade - 3) * (timer_fade - 3)) ;
                       
                        if (timer_fade > 11) { lerpcol.a = 0; avancement_moment = false; timer_fade = 1; avancement_pos ++; }
                        avancement.color = lerpcol;
                        
                    }

                   

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
                        if (!inversed) LetterAction.color = new Color(0, 127f, 255f);
                        else LetterAction.color = new Color(0f, 1f, 0f);
                        RightLeg.sprite = SpritesLegs[ActionState % 6];
                        LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];
                    }
                    else if (Is_Player2Playing )
                    {
                        if (!inversed) LetterAction.color = new Color (0f, 1f, 0f);
                        else LetterAction.color = new Color(0, 127f, 255f);
                        RightLeg.sprite = SpritesLegs[ActionState % 6];
                        LeftLeg.sprite = SpritesLegs[(ActionState + 3) % 6];

                    }
                    vitesseDeplacement = InitvitesseDeplacement + (Streak / 2);
                    if (Is_Player1Playing || Is_Player2Playing)
                    {
                        LetterAction.text = CharActionList[ActionState % 6];
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
                            
                            ActionState++;
                            if (ActionState % 3 == 1) { effect.clip = sound_1; effect.Play(); }
                            if (ActionState % 3 == 2) { effect.clip = sound_2; effect.Play(); }
                            if (ActionState % 3 == 0 && Streak != 0) { effect.clip = sound_3; effect.Play(); }
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
                                    transform.position = zone_slow.transform.position - new Vector3(1, 0, 0);
                                Streak++;
                            }
                        }
                        else if (Input.anyKeyDown && !Input.GetKeyDown(touche))
                        {
                            effect.volume = 0.5f; effect.clip = sound_fail; effect.Play(); effect.volume = 0.10f;
                            StreakAction.color = Color.red;
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
                            Color lerpedColor = Color.Lerp(Color.white, Color.yellow, t);
                            StreakAction.color = lerpedColor;
                        }
                      
                    }
                }
                else
                {

                    LetterAction.text = "";
                   
                    if (obst.tag_o == "high")
                    {
                        ObstacleAvoiding = true;
                        LetterAvoidL.text = "Down";
                        LetterAvoidR.text = "S";
                        if (!arrow_first && Input.GetKeyDown(esquiv_haut[1])) // en gros, je vois quelle touche a été pressée en premier, et j'attend que l'autre soit pressée
                        {
                            s_first = true;
                            
                        }

                        if (!s_first && Input.GetKeyDown(esquiv_haut[0]))
                        {
                            arrow_first = true;
                            
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
                        LetterAvoidL.text = "Up";
                        LetterAvoidR.text = "Z";
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
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");

            }
            if (transform.position.x >= arrive)
            { audio_level.pitch = 1; bravo.text = "BRAVO !!" + "\n" + "Veuillez donner votre temps au staff puis appuyez sur Echap pour revenir au menu"; Chrono.fontSize = 5  ; Chrono.text = "Temps final : " + chrono + " secondes "; Chrono.color = Color.red; LetterAction.text = ""; if (audio_level.clip != victory) { audio_level.clip = victory; audio_level.loop = false; audio_level.Play(); } ScoreScreenshot.CreateScreenshot(); }
            else { bravo.text = "Pause, appuyez sur échap pour revenir au menu et espace pour reprendre"; if (Input.GetKeyDown(KeyCode.Space)) { termine = false; bravo.text = ""; } }

        }
    }
}

