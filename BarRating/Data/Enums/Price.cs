using System.ComponentModel.DataAnnotations;

namespace BarRating.Data.Enums
{
    public enum PriceCategory
    {
        [Display(Name = "From 1 to 10")]
        From1to10 = 0,
        [Display(Name = "From 10 to 20")]
        From10to20 = 1,
        [Display(Name = "From 20 to 30")]
        From20to30 = 2,
        [Display(Name = "From 30 to 40")]
        From30to40 = 3,
        [Display(Name = "From 40 to 50")]
        From40to50 = 4,
        [Display(Name = "From 50 to 60")]
        From50to60 = 5,
        [Display(Name = "From 60 to 70")]
        From60to70 = 6,
        [Display(Name = "From 70 to 80")]
        From70to80 = 7,
        [Display(Name = "From 80 to 90")]
        From80to90 = 8,
        [Display(Name = "From 90 to 100")]
        From90to100 = 9,
        [Display(Name = "More than 100")]
        MoreThan100 = 10,
        [Display(Name = "None")]
        None = 11

    }
}
