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
    [SerializeField] private AudioClip journalPageTurnSound;
    private string[] journalEntries = {"I can’t believe I forgot to give the xenomouse the ability to breathe the air in the test chamber! How can it complete the field test without being able to breathe?? Goddamn it!! Well, at least this should help keep my hubris in check. Probably.","Xenomouse did a lot better now that it can breathe. It seems a little dumb still. Will need to keep working at it.","Xenomouse’s heart seems to give out before it can do much… I’m hoping to improve that in the future. But in the meantime, I shall brush its silky hair and tell it what a good xenomouse it is! Soon, you shall die, little friend. But it’s all in the name of science!! (You can’t understand that, but I promise science is cool!)","Xenomouse is slowly progressing. Unfortunately, slow isn’t really what I’m looking for! I was thinking of getting some cattle prods, but I promised my supervisor I’d actually follow the ethics waver I signed this time."};

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
            SoundEffects.Instance.PlayClip(journalPageTurnSound);
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
