using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    public List<Sprite> SpritesObst;
    public SpriteRenderer Obst;
    private movement movement;
    // Start is called before the first frame update
    void Start()
    {
        Obst.sprite = SpritesObst[0];
        movement = GameObject.Find("Player").GetComponent<movement>();
    }

    float timer = 0f;
    int compteur = 0;
    // Update is called once per frame
    void Update()
    {
        if (!movement.termine)
        {
            timer += Time.deltaTime;
            float limit = 0.1f / slow_mo.slow;
            if (timer > limit)
            {
                compteur++;
                Obst.sprite = SpritesObst[compteur % SpritesObst.Count];
                timer = 0f;
            }
        }
    }
}
