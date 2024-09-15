using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Animator animator;
    public TextMeshPro text;
    public GameObject cutscene;
    [SerializeField] public BoxCollider2D skipButton;
    [SerializeField] public Camera camera1;
    [SerializeField] public GameObject loading;
    [SerializeField] public GameObject mainMenu;

    public List<string> sentences = new List<string>();
    public int index = 0;
    public bool isFinished = false;
    void Start()
    {
        initSentence();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = camera1.ScreenToWorldPoint(Input.mousePosition);
            if(skipButton.OverlapPoint(mousePos))
            {
                Debug.Log("Skip");
                FinishCutscene();
            }
        }
    }

    private void initSentence()
    {
        sentences.Add("Under the vast branches of Yggdrasil, life flourishes. The nature live in harmony, nurtured by its ancient magic.");
        sentences.Add("But peace doesn't last. Mysterious meteors fall from the sky, crashing into the earth. From the wreckage, slimes emerge, drawn to Yggdrasil’s power.");
        sentences.Add("Yggdrasil senses the danger and awakens its magic. It releases seeds from its branches, infused with energy. These plants will grow to protect the land.");
        sentences.Add("In need of dire help, Yggdrasil opens a portal to summon a protector from another world, to plant those magical seeds.");
        sentences.Add("As the slimes draw near, the plants are ready for battle. The fight to save Yggdrasil begins now.");
        text.text = sentences[0];
    }

    public void nextSentence()
    {
        if (index < sentences.Count - 1)
        {
            index++;
            text.text = sentences[index];
        }
    }

    public void FinishCutscene()
    {
        isFinished = true;
        cutscene.SetActive(false);
        loading.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void StartCutscene()
    {
        cutscene.SetActive(true);
        animator.SetBool("scene1",true);
    }
}
