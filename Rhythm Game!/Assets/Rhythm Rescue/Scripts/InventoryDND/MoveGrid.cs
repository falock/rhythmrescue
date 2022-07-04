using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveGrid : MonoBehaviour
{

    [SerializeField] private GameObject[] positions;
    public List<GameObject> cards = new List<GameObject>();
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject leftButton;
    //public bool hasStarted = false;
   // public bool hasBeenRefreshed = false;

    public int trackingInt = 0;
    /*
    private void OnEnable()
    {
        /*
        for (int i = 0; i < this.transform.childCount; i++)
        {
            cards.Add(this.transform.GetChild(i).gameObject);
        }

        hasStarted = true;
        trackingInt = 0;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (!cards.Contains(this.transform.GetChild(i).gameObject))
            {
                cards.Add(this.transform.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                cards[i].transform.position = positions[6].transform.position;
            }
        }
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null && i <= 5)
            {
                cards[i].transform.position = positions[i + 1].transform.position;
            }
            else if (cards[i] != null && i > 5)
            {
                cards[i].transform.position = positions[6].transform.position;
            }
        }

        leftButton.GetComponent<Button>().enabled = false;
        leftButton.GetComponent<Image>().enabled = false;

        if (5 > cards.Count)
        {
            rightButton.GetComponent<Button>().enabled = false;
            rightButton.GetComponent<Image>().enabled = false;
        }
        else if (5 < cards.Count)
        {
            rightButton.GetComponent<Button>().enabled = true;
            rightButton.GetComponent<Image>().enabled = true;
        }
        

        trackingInt = 0;
        cards.Clear();

        for (int i = 1; i <= this.transform.childCount; i++)
        {
            Debug.Log("childcount i = " + i);
            
            if (!cards.Contains(this.transform.GetChild(i).gameObject))
            {
                cards.Add(this.transform.GetChild(i).gameObject);
            }
            
            cards.Add(this.transform.GetChild(i - 1).gameObject);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                cards[i].transform.position = positions[6].transform.position;
            }
        }

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null && i <= 5)
            {
                cards[i].transform.position = positions[i + 1].transform.position;
            }
            else if (cards[i] != null && i > 5)
            {
                cards[i].transform.position = positions[6].transform.position;
            }
        }

        leftButton.GetComponent<Button>().enabled = false;
        leftButton.GetComponent<Image>().enabled = false;

        if (5 > cards.Count)
        {
            rightButton.GetComponent<Button>().enabled = false;
            rightButton.GetComponent<Image>().enabled = false;
        }
        else if (5 < cards.Count)
        {
            rightButton.GetComponent<Button>().enabled = true;
            rightButton.GetComponent<Image>().enabled = true;
        }
        */
    
    
    public void RefreshUI()
    {
        if (this.gameObject.activeInHierarchy)
        {
            trackingInt = 0;
            cards.Clear();

            for (int i = 1; i <= this.transform.childCount; i++)
            {
                cards.Add(this.transform.GetChild(i -1).gameObject);
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] != null)
                {
                    cards[i].transform.position = positions[6].transform.position;
                }
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] != null && i <= 5)
                {
                    cards[i].transform.position = positions[i + 1].transform.position;
                }
                else
                {
                    cards[i].transform.position = positions[6].transform.position;
                }
            }

            leftButton.GetComponent<Button>().enabled = false;
            leftButton.GetComponent<Image>().enabled = false;

            if (5 >= cards.Count)
            {
                rightButton.GetComponent<Button>().enabled = false;
                rightButton.GetComponent<Image>().enabled = false;
            }
            else if (5 < cards.Count)
            {
                rightButton.GetComponent<Button>().enabled = true;
                rightButton.GetComponent<Image>().enabled = true;
            }

            trackingInt = 0;
            cards.Clear();

            for (int i = 1; i <= this.transform.childCount; i++)
            {
                /*
                if (!cards.Contains(this.transform.GetChild(i).gameObject))
                {
                    cards.Add(this.transform.GetChild(i).gameObject);
                }
                */
                cards.Add(this.transform.GetChild(i - 1).gameObject);
            }
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] != null)
                {
                    cards[i].transform.position = positions[6].transform.position;
                }
            }
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] != null && i <= 5)
                {
                    cards[i].transform.position = positions[i + 1].transform.position;
                }
                else
                {
                    cards[i].transform.position = positions[6].transform.position;
                }
            }

            leftButton.GetComponent<Button>().enabled = false;
            leftButton.GetComponent<Image>().enabled = false;

            if (5 >= cards.Count)
            {
                rightButton.GetComponent<Button>().enabled = false;
                rightButton.GetComponent<Image>().enabled = false;
            }
            else if (5 < cards.Count)
            {
                rightButton.GetComponent<Button>().enabled = true;
                rightButton.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void Update()
    {
        trackingInt = 0;
        cards.Clear();

        for (int i = 1; i <= this.transform.childCount; i++)
        {
            /*
            if (!cards.Contains(this.transform.GetChild(i).gameObject))
            {
                cards.Add(this.transform.GetChild(i).gameObject);
            }
            */
            cards.Add(this.transform.GetChild(i -1).gameObject);
        }

        // move them all off screen
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                cards[i].transform.position = positions[6].transform.position;
            }
        }

        // 
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null && i <= 5)
            {
                cards[i].transform.position = positions[i + 1].transform.position;
            }
            else if (cards[i] != null && i > 5)
            {
                cards[i].transform.position = positions[6].transform.position;
            }
        }

        leftButton.GetComponent<Button>().enabled = false;
        leftButton.GetComponent<Image>().enabled = false;

        if (5 > cards.Count)
        {
            rightButton.GetComponent<Button>().enabled = false;
            rightButton.GetComponent<Image>().enabled = false;
        }
        else if (5 < cards.Count)
        {
            rightButton.GetComponent<Button>().enabled = true;
            rightButton.GetComponent<Image>().enabled = true;
        }
    }

    public void Right()
    {
        if (trackingInt + 5 < cards.Count)
        {
            trackingInt++;
            
            cards[trackingInt - 1].GetComponent<RectTransform>().position = Vector3.MoveTowards(cards[trackingInt - 1].transform.position, new Vector2(positions[0].transform.position.x, positions[0].transform.position.y), 1 * Time.deltaTime);
            cards[trackingInt].GetComponent<RectTransform>().position = Vector3.MoveTowards(cards[trackingInt].transform.position, new Vector2(positions[1].transform.position.x, positions[1].transform.position.y), 0.1f);
            cards[trackingInt +1].GetComponent<RectTransform>().position = Vector3.MoveTowards(cards[trackingInt +1].transform.position, new Vector2(positions[2].transform.position.x, positions[2].transform.position.y), 0.1f);
            cards[trackingInt +2].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(cards[trackingInt +2].transform.position, new Vector2(positions[3].transform.position.x, positions[3].transform.position.y), 0.1f);
            cards[trackingInt +3].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(cards[trackingInt +3].transform.position,  new Vector2(positions[4].transform.position.x, positions[4].transform.position.y), 0.1f);
            cards[trackingInt +4].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(cards[trackingInt +4].transform.position,  new Vector2(positions[5].transform.position.x, positions[5].transform.position.y), 0.1f);
            cards[trackingInt +5].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(cards[trackingInt +5].transform.position,  new Vector2(positions[6].transform.position.x, positions[6].transform.position.y), 0.1f);
            leftButton.GetComponent<Button>().enabled = true;
            leftButton.GetComponent<Image>().enabled = true;
            
            //StartCoroutine("UICo");
            //cards[trackingInt ].GetComponent<FriendCardMove>().Move(positions[i].transform.position.x);
            
            cards[trackingInt - 1].GetComponent<MoveCard>().Move(positions[0].transform.position.x);
            cards[trackingInt].GetComponent<MoveCard>().Move(positions[1].transform.position.x);
            cards[trackingInt + 1].GetComponent<MoveCard>().Move(positions[2].transform.position.x);
            cards[trackingInt + 2].GetComponent<MoveCard>().Move(positions[3].transform.position.x);
            cards[trackingInt + 3].GetComponent<MoveCard>().Move(positions[4].transform.position.x);
            cards[trackingInt + 4].GetComponent<MoveCard>().Move(positions[5].transform.position.x);
            cards[trackingInt + 5].GetComponent<MoveCard>().Move(positions[6].transform.position.x);

            leftButton.GetComponent<Button>().enabled = true;
            leftButton.GetComponent<Image>().enabled = true;

            if (trackingInt + 5 >= cards.Count)
            {
                rightButton.GetComponent<Button>().enabled = false;
                rightButton.GetComponent<Image>().enabled = false;
            }
        }
    }

    public void Left()
    {
        if (trackingInt != 0)
        {
            trackingInt--;
            if(trackingInt != 0)
            {
                cards[trackingInt - 1].GetComponent<MoveCard>().Move(positions[0].transform.position.x);
            }
            cards[trackingInt].GetComponent<MoveCard>().Move(positions[1].transform.position.x);
            cards[trackingInt + 1].GetComponent<MoveCard>().Move(positions[2].transform.position.x);
            cards[trackingInt + 2].GetComponent<MoveCard>().Move(positions[3].transform.position.x);
            cards[trackingInt + 3].GetComponent<MoveCard>().Move(positions[4].transform.position.x);
            cards[trackingInt + 4].GetComponent<MoveCard>().Move(positions[5].transform.position.x);
            cards[trackingInt + 5].GetComponent<MoveCard>().Move(positions[6].transform.position.x);
            rightButton.GetComponent<Button>().enabled = true;
            rightButton.GetComponent<Image>().enabled = true;

            if (trackingInt == 0)
            {
                leftButton.GetComponent<Button>().enabled = false;
                leftButton.GetComponent<Image>().enabled = false;
            }
        }
    }

}
