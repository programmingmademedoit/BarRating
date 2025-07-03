using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarRating.Data.Entities
{
    [Flags]
    public enum BarType
    {
        None = 0,
        Pub = 1 << 0,
        SportsBar = 1 << 1,
        CocktailBar = 1 << 2,
        WineBar = 1 << 3,
        DiveBar = 1 << 4,
        Lounge = 1 << 5,
        Nightclub = 1 << 6,
        RooftopBar = 1 << 7,
        BeachBar = 1 << 8,
        KaraokeBar = 1 << 9,
        TikiBar = 1 << 10,
        Speakeasy = 1 << 11,
        Brewpub = 1 << 12,
        WhiskeyBar = 1 << 13,
        HookahBar = 1 << 14,
        GayBar = 1 << 15,
        PianoBar = 1 << 16,
        IceBar = 1 << 17,
        Other = 1 << 18
    }

}
