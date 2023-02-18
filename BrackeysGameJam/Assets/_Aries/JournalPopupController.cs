using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalPopupController : MonoBehaviour
{
    [SerializeField] private Button previousButton, nextButton;
    [SerializeField] private TextMeshProUGUI journalText, journalTitle;
    [SerializeField] private Button closeButton;
    [SerializeField] private AudioClip journalPageTurnSound, journalCloseSound;
    private string[] journalEntries = {"The xenomouse performed fairly… badly on the first test. Hopefully, it will improve on future tests. Lengthening its lifespan would probably help.","Xenmouse is performing slightly better. It seems a little dumb still. Will need to keep working at it.","Xenomouse’s heart seems to give out before it can do much… I’m hoping to improve that in the future. But in the meantime, I shall brush its silky hair and tell it what a good xenomouse it is! Soon, you shall die, little friend. But it’s all in the name of science!! (You can’t understand that, but I promise science is cool!)","Xenomouse is slowly progressing. Unfortunately, slow isn’t really what I’m looking for! I was thinking of getting some cattle prods, but I promised my supervisor I’d actually follow the ethics waver I signed this time.","A peer of mine, Corey said that the xenomouse will never work! That a weird mutant mouse who can run through mazes, avoid enemies, and collect cheese could never save the world! I always thought Corey was smart, but he is clearly going against the flow of progress and trying to prevent an inevitable future. And that’s pretty dumb!\n\n In any case, I’ll show him!!!! Xenomouse is going to solve the maze and save the world, or it’ll die trying !!","I’ve grown attached to the xenomouse. I’ve decided to name it Babs. Hello, Babs 7! Who’s a cutie batootie xenomousie?","Corey is still pushing his “your mutant mouse can’t save the world” narrative. Well, he isn’t here in the lab watching the beautiful death and rebirth of Babs. He would never understand the beauty and power I am harnessing! One day, Babs will kill Corey and eat him.\n\n(If any cops are reading this, that was a joke. I’m 100% joking. No need to look deeper into this.)","Not going to lie, Babs 8 was a bit of a disappointment… Hopefully Babs 9 will be better! You can do it little guy!!","Corey has started investigating my work for violating ethical research standards. I told him to shut up and go away. I’m sure that taught him!","I’ve recently developed something that I think will help Babs get through the field test! I’m not sure when (or if) I’ll ever finish it, but once it’s done it’ll attach to Babs’s heart and revive it once it dies. Cool, right? \n\nMy supervisor said that if I can get it to work on humans, I could make a ton of money or win a Nobel prize, but I’m thinking bigger than that! Babs is going to save the world!!","Progress is going well! My supervisor says I shouldn’t rush things but who ever got somewhere going slowly and carefully? Well… lots of people actually. But they’re not as flashy as I am! I will become the true master of life and death! …Over a xenomouse. Yeah. These logs are private, right?","Okay, I seem to be unable to edit my previous logs. This is for “thorough recordkeeping and honesty” or some other such nonsense garbage. This is ridiculous! Also, probably not the best look for me… Hey, if you’re reading this, please stop. These were supposed to be for my private thoughts. I promise anything bad I said, it was definitely not serious. These logs should not be used to incriminate me under any circumstances. Especially not the fact that twelve of my test subjects have died!! They’re not even really dead if you think about it. So, I have nothing to hide! Uhh… But still don’t read these, thanks.","Corey has brought my work before an ethics committee?? Like, seriously not cool! I showed them I was well within the parameters established when I set out my research. Then, they asked who in their right mind authorized these parameters. So, that’s not looking great! \n\nMy supervisor said they’d tell the committee about the human heart stuff and it would all get swept under the rug though. Usually projects are suspended during investigations, but luckily not mine! I’ve been instructed to keep my head down and keep working, so that’s what I’m doing.\n\nP.S. Screw off, Corey you jerk.","Sometimes it’s hard for me to see the improvement, but when I compare Babs 14 and Babs 1, it’s really night and day! Nice going, me!! And also Babs, I guess!","I had a thought that Babs might do better with some positive encouragement, so I decided to start shouting “You’ve got this!” while it tries the field test. We’ll see if it helps!","Or, I guess you don’t have this… But I can tell we’re getting a lot closer. You’ll make it out soon, Babs."};

    private int currentJournalEntry = 0;
    private int maxEntriesToDisplay = 3;

    private void Start() {
        maxEntriesToDisplay = GameManager.instance.GetGenerationCount();
        if(maxEntriesToDisplay > journalEntries.Length)
            maxEntriesToDisplay = journalEntries.Length;
        previousButton.onClick.AddListener(() => {
            SoundEffects.Instance.PlayClip(journalPageTurnSound);
            AnimationHelper.pressButton(previousButton.transform,delegate(){
                currentJournalEntry--;
                if(currentJournalEntry < 0)
                    currentJournalEntry = 0;
                UpdateJournal();
            });
        });
        nextButton.onClick.AddListener(() => {
            SoundEffects.Instance.PlayClip(journalPageTurnSound);
            AnimationHelper.pressButton(nextButton.transform,delegate(){
                currentJournalEntry++;
                if(currentJournalEntry >= journalEntries.Length || currentJournalEntry >= maxEntriesToDisplay)
                {
                    currentJournalEntry = maxEntriesToDisplay - 1;
                }
                UpdateJournal();
            });
        });
        closeButton.onClick.AddListener(() => {
            SoundEffects.Instance.PlayClip(journalCloseSound);
            AnimationHelper.pressButton(closeButton.transform,delegate(){
                gameObject.SetActive(false);
            });
        });
    }

    private void OnEnable() {
        SoundEffects.Instance.PlayClip(journalPageTurnSound);
        UpdateJournal();
    }

    private void UpdateJournal(){
        journalText.text = journalEntries[currentJournalEntry];
        journalTitle.text = "LOG " + (currentJournalEntry + 1);
    }

}
