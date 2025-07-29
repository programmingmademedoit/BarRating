using System.ComponentModel.DataAnnotations;

namespace BarRating.Data.Enums
{
    public enum NumberOfPeople
    {
        [Display(Name = "1")]
        One = 1,

        [Display(Name = "1-2 People")]
        OneToTwo = 2,

        [Display(Name = "3-4 People")]
        ThreeToFour = 3,

        [Display(Name = "5-8 People")]
        FiveToEight = 6,

        [Display(Name = "9+ People")]
        NinePlus = 7,

        [Display(Name = "Appropriate for all types of groups")]
        AllGroups = 4,

        [Display(Name = "Not sure")]
        NotSure = 5,

    }
}
