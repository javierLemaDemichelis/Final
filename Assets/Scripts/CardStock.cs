using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStock
{
    public CardStock(CardInfo _cardInfo, int count)
    {
        this.cardInfo = _cardInfo;
        this.count = count;
    }
    private int  count  = 0;
    private int cardUsed = 0;
    private CardInfo cardInfo = null;
    public void SetCount(int _count) 
    {
        this.count = _count;
    }
    public int  GetCount()
    {
        return this.count;
    }
    public CardInfo GetCardInfo()
    {
        return this.cardInfo;
    }
    public void SetCardsUsed(int num) 
    {
        this.cardUsed = num;
    }
    public int GetCardsUsed()
    {
        return this.cardUsed;
    }
    public void UseCard() 
    {
        cardUsed++;
    }
}
