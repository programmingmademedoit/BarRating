using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BarRating.Data.Enums
{
    public enum BarType
    {
        None = 0,
        [Display(Name = "Pub")]
        Pub = 1 << 0,

        [Display(Name = "Sports Bar")]
        SportsBar = 1 << 1,

        [Display(Name = "Cocktail Bar")]
        CocktailBar = 1 << 2,

        [Display(Name = "Wine Bar")]
        WineBar = 1 << 3,

        [Display(Name = "Dive Bar")]
        DiveBar = 1 << 4,

        [Display(Name = "Lounge")]
        Lounge = 1 << 5,

        [Display(Name = "Nightclub")]
        Nightclub = 1 << 6,

        [Display(Name = "Rooftop Bar")]
        RooftopBar = 1 << 7,

        [Display(Name = "Beach Bar")]
        BeachBar = 1 << 8,

        [Display(Name = "Karaoke Bar")]
        KaraokeBar = 1 << 9,

        [Display(Name = "Tiki Bar")]
        TikiBar = 1 << 10,

        [Display(Name = "Speakeasy")]
        Speakeasy = 1 << 11,

        [Display(Name = "Brewpub")]
        Brewpub = 1 << 12,

        [Display(Name = "Whiskey Bar")]
        WhiskeyBar = 1 << 13,

        [Display(Name = "Hookah Bar")]
        HookahBar = 1 << 14,

        [Display(Name = "Gay Bar")]
        GayBar = 1 << 15,

        [Display(Name = "Piano Bar")]
        PianoBar = 1 << 16,

        [Display(Name = "Ice Bar")]
        IceBar = 1 << 17,

        [Display(Name = "Beer Pub")]
        BeerPub = 1 << 18,

        [Display(Name = "Other")]
        Other = 1 << 19
    }

}
