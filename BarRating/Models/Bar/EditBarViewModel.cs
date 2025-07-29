using BarRating.Data.Entities;
using BarRating.Data.Enums;
using BarRating.Models.Schedule;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BarRating.Models.Bar
{
    public class EditBarViewModel
    {
        public int BarId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile? Image { get; set; }  // New image upload
        public string? URL { get; set; }        // Current image URL

        public string Location { get; set; }
        public BarType BarType { get; set; }
        public string? Website { get; set; }
        public string PhoneNumber { get; set; }
        public string? Instagram { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Schedule
        public List<BarScheduleViewModel> Schedules { get; set; }
        public List<ScheduleOverrideViewModel> ScheduleOverrides { get; set; }

        // Features
        public bool HasLiveMusic { get; set; }
        public bool HasFood { get; set; }
        public bool HasOutdoorSeating { get; set; }
        public bool AcceptsReservations { get; set; }
        public bool HasParking { get; set; }
        public bool IsWheelchairAccessible { get; set; }

        // Verification
        public bool IsVerified { get; set; }


    }
}