using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for handling the karma system.
/// </summary>
public class Karma
{
    [SerializeField] private int _orderKarma;
    [SerializeField] private int _freedomKarma;
    [SerializeField] private Player _player;

    public int OrderKarma => _orderKarma;
    public int FreedomKarma => _freedomKarma;
    public Player Player => _player;

    public Karma(Player player)
    {
        _player = player;
        _orderKarma = default;
        _freedomKarma = default;
    }

    public void OrderChoice(int karmaBoost)
    {
        _orderKarma += karmaBoost;
        _freedomKarma -= Mathf.RoundToInt(karmaBoost/2);
    }

    public void FreedomChoice(int karmaBoost)
    {
        _freedomKarma += karmaBoost;
        _orderKarma -= Mathf.RoundToInt(karmaBoost/2);
    }

    public void NeutralChoice(int karmaLoss)
    {
        _freedomKarma -= karmaLoss;
        _orderKarma -= karmaLoss;
    }
}
