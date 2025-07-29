using System.ComponentModel.DataAnnotations;
namespace BarRating.Data.Enums
{
    [Flags]
    public enum Tags
    {
        None = 0,

        [Display(Name = "Cozy")]
        Cozy = 1 << 0,
        [Display(Name = "Trendy")]
        Trendy = 1 << 1,
        [Display(Name = "Elegant")]
        Elegant = 1 << 2,
        [Display(Name = "Loud")]
        Loud = 1 << 3,
        [Display(Name = "Quiet")]
        Quiet = 1 << 4,
        [Display(Name = "Romantic")]
        Romantic = 1 << 5,
        [Display(Name = "Friendly Staff")]
        FriendlyStaff = 1 << 6,
        [Display(Name = "Good for Groups")]
        GoodForGroups = 1 << 7,
        [Display(Name = "Good for Dates")]
        GoodForDates = 1 << 8,
        [Display(Name = "Lively")]
        Lively = 1 << 9,
        [Display(Name = "Relaxing")]
        Relaxing = 1 << 10,

        [Display(Name = "Great Cocktails")]
        GreatCocktails = 1 << 11,
        [Display(Name = "Craft Beer")]
        CraftBeer = 1 << 12,
        [Display(Name = "Wide Drink Selection")]
        WideDrinkSelection = 1 << 13,
        [Display(Name = "Delicious Snacks")]
        DeliciousSnacks = 1 << 14,
        [Display(Name = "Affordable Prices")]
        AffordablePrices = 1 << 15,
        [Display(Name = "Expensive")]
        Expensive = 1 << 16,
        [Display(Name = "Good Happy Hour")]
        GoodHappyHour = 1 << 17,
        [Display(Name = "Great Music")]
        GreatMusic = 1 << 18,
        [Display(Name = "Live Music")]
        LiveMusic = 1 << 19,
        [Display(Name = "Clean")]
        Clean = 1 << 20,
        [Display(Name = "Fast Service")]
        FastService = 1 << 21,
        [Display(Name = "Slow Service")]
        SlowService = 1 << 22,

        [Display(Name = "Young Crowd")]
        YoungCrowd = 1 << 23,
        [Display(Name = "Mature Crowd")]
        MatureCrowd = 1 << 24,
        [Display(Name = "Tourist Friendly")]
        TouristFriendly = 1 << 25,
        [Display(Name = "LGBTQ+ Friendly")]
        LGBTQFriendly = 1 << 26,
        [Display(Name = "Local's Spot")]
        LocalsSpot = 1 << 27,

        [Display(Name = "Nice View")]
        NiceView = 1 << 28,
        [Display(Name = "Outdoor Seating")]
        OutdoorSeating = 1 << 29,
        [Display(Name = "Themed Interior")]
        ThemedInterior = 1 << 30,
        [Display(Name = "Unique Decor")]
        UniqueDecor = 1 << 31
    }
}
