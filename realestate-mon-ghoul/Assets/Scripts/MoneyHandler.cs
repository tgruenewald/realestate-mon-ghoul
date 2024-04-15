using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private GameObject turnIndicator;
    [SerializeField] private GameObject hint;
    private Texture2D   childCursor;
    private Texture2D teenCursor;
    private Texture2D demonCursor;

    [SerializeField] private Sprite childIcon;
    [SerializeField] private Sprite teenIcon;
    [SerializeField] private Sprite demonIcon;
    [SerializeField] private Sprite newsIcon;
    [SerializeField] private Image avatar;
    [SerializeField] private TMP_Text flavorText;
    [SerializeField] private AudioSource beepHigh;
    [SerializeField] private AudioSource beepLow;
    public TMP_Text moneyAmount;

    private string[] childQuips = { "Wow! You're gonna give me 10 dollars to terrorize my neighbors! That's more then I get for mowing lawns. Thanks!",
    "You know, if you have us go to houses that are further apart, then it'll take longer before those stupid Ghost Dusters can catch up with us.",
    "Scaring people is so much fun! Last night I made a guy wet his pants. And I can buy a new game for my Coleco, best deal EVER!!",
    "The last house you sent me to was actually my aunt's place. She was really upset. I don't think she's gonna take me to see Star Wars on my birthday now.",
    "What's a condo anyway? And why do you have to buy up a bunch of houses for one? Is it cooler then Pitfall? That's the best thing ever!"};

    private string[] teenQuips = { "Why are you hiring those dumb kids? I can summon a real ghost. It'll keep those Ghost Dusters busy for a lot longer then some kid can.",
    "I went to a Killing Joke concert last night. It was great. You wouldn't understand their music, though. They don't make music for old people.",
    "I can't believe you're buying so many houses. You know, the more houses you buy the more they're gonna cost. That's just basic economics. It's weird you don't already know.",
    "The AC in my place broke. You're gonna have to pay a fortune to fix it, since you're the landlord now.  Yeah, I mean my parent's place, obviously I live there too.",
    "I was finally able to buy some real eyeliner. Now I won't have to use a marker. The lady in the store threatened not to sell it to me. She said that satanists shouldn't be allowed to go shopping. I don't even worship Satan. I worship Baal."};

    private string[] demonQuips = { "I can summon many a powerful beast for your purposes. Even this... condo building you speak of can be accomplished.",
    "The Ghost Dusters get faster with every haunting, but worry not. The variety of demons I summon will always confuse them for some time.",
    "There is a great price to pay for the power I have. You are lucky I am selling it to you for only money."};

    private string[] headlines = { "Halloween Comes Early? Children seen dressing up in costumes and roaming the neighborhood",
    "Recent Report of Ghost Found To Be Just A Child Under Sheet",
    "Woman Reports Satanist Sneaking into her Home: Is Your Home Safe?",
    "Creep Seen Giving Money to Children In Exchange for Watching Them Dress In Costumes",
    "Housing Prices Ridiculously Low: Homes sell for as low as $150",
    "Local Idiot Thinks That In 40 Years A Small House Will Cost Over $100,000; 100 Times The Prices of Today.",
    "Do All Goths Worship Satan? We Ignore Local Goths To Bring You an Interview With Someone Who Knows Nothing About Satanism or Goth Subculture",
    "Ghost Duster's Business on Rise: How the New Movie Has Effected Local Ghost Sitings",
    "How Many Homes Does One Person Need? Local Landlord Keeps Buying",
    "Local Goth Recruiting Other Teens For Satanic Ritual: They Say They're Being Paid to by Their Landlord"};

    void Start (){
        childCursor = Resources.Load<Texture2D>("childPointer");
        teenCursor = Resources.Load<Texture2D>("ghostPointer");
        demonCursor = Resources.Load<Texture2D>("demonPointer");

        Debug.Log("ghost cursor: " +  teenCursor);

    }
    void Update()
    {
        if (gameState != null)
        {
            moneyAmount.text = gameState.getFunds().ToString();
            turnIndicator.GetComponent<TMP_Text>().text = "Turn " + gameState.turnNumber + " of " + gameState.maxTurns;
            if (gameState.turnNumber >= gameState.maxTurns)
            {
                turnIndicator.GetComponent<TMP_Text>().text = "GAME OVER";
            }

            hint.GetComponent<TMP_Text>().text = gameState.getHint();
        }

    }
    public void PurchaseGhost(int price){
        if (gameState.playerAction)
        {

            if (price == 0)
            {
                Debug.Log("Skipping turn");
                gameState.playerAction = false;
                avatar.sprite = newsIcon;
                flavorText.text = headlines[UnityEngine.Random.Range(0,headlines.Length)];
            } else
            {
                if (gameState.getFunds() >= price && gameState.getGhostLevel() == 0)
                {  
                    beepHigh.Play();
                    if(price == 10)
                    {
                        Cursor.SetCursor(childCursor, Vector2.zero, CursorMode.Auto);
                        avatar.sprite = childIcon;
                        flavorText.text = childQuips[UnityEngine.Random.Range(0, childQuips.Length)];
                    }
                    else if(price == 100)
                    {
                        Cursor.SetCursor(teenCursor, Vector2.zero, CursorMode.Auto);
                        avatar.sprite = teenIcon;
                        flavorText.text = teenQuips[UnityEngine.Random.Range(0, teenQuips.Length)];
                    }
                    else
                    {
                        Cursor.SetCursor(demonCursor, Vector2.zero, CursorMode.Auto);
                        avatar.sprite = demonIcon;
                        flavorText.text = demonQuips[UnityEngine.Random.Range(0, demonQuips.Length)];
                    }
                    
                    gameState.setHint("Pick a house to haunt");
                    gameState.setGhostLevel(price);
                    gameState.deductFunds(price);
                    moneyAmount.text = gameState.getFunds().ToString();
                }
                else
                {
                    beepLow.Play();
                }

            }

        }

    }
}
