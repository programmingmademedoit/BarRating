using BarRating.Data.Entities;
namespace BarRating.Data.Entities
{
    [Flags]
    public enum Tags
    {
        None = 0,

        // Atmosphere / Vibe
        Cozy = 1 << 0,
        Trendy = 1 << 1,
        Elegant = 1 << 2,
        Loud = 1 << 3,
        Quiet = 1 << 4,
        Romantic = 1 << 5,
        FriendlyStaff = 1 << 6,
        GoodForGroups = 1 << 7,
        GoodForDates = 1 << 8,
        Lively = 1 << 9,
        Relaxing = 1 << 10,

        // Offerings / Experience
        GreatCocktails = 1 << 11,
        CraftBeer = 1 << 12,
        WideDrinkSelection = 1 << 13,
        DeliciousSnacks = 1 << 14,
        AffordablePrices = 1 << 15,
        Expensive = 1 << 16,
        GoodHappyHour = 1 << 17,
        GreatMusic = 1 << 18,
        LiveMusic = 1 << 19,
        Clean = 1 << 20,
        FastService = 1 << 21,
        SlowService = 1 << 22,

        // Crowd / Social
        YoungCrowd = 1 << 23,
        MatureCrowd = 1 << 24,
        TouristFriendly = 1 << 25,
        LGBTQFriendly = 1 << 26,
        LocalsSpot = 1 << 27,

        // Extras
        NiceView = 1 << 28,
        OutdoorSeating = 1 << 29,
        ThemedInterior = 1 << 30,
        UniqueDecor = 1 << 31
    }
}
