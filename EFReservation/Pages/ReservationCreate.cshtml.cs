using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFReservation.Models;
using Microsoft.Extensions.Logging;

namespace EFReservation.Pages
{
    public class ReservationCreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReservationCreateModel> _logger;

        public ReservationCreateModel(AppDbContext context, ILogger<ReservationCreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public SelectList Rooms { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
            Reservation = new Reservation();
            Reservation.Room = new Room();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ViewData.ModelState)
                {
                    var key = modelState.Key;
                    var errors = modelState.Value.Errors;

                    foreach (var error in errors)
                    {
                        _logger.LogWarning($"Error in {key}: {error.ErrorMessage}");
                    }
                }

                Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
                return Page();
            }

            // Kullanıcı kimliğini kimlik doğrulama sisteminden al
            var userId = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous";
            Reservation.UserId = userId;

            // RoomName alanını validasyondan çıkarıyoruz
            ModelState.Remove("Reservation.Room.RoomName");

            _context.Reservations.Add(Reservation);
            _logger.LogInformation("Reservation added to context.");

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Reservation saved to database.");

                var log = new Log
                {
                    UserId = userId,
                    Action = "Created Reservation",
                    Timestamp = DateTime.Now
                };
                _context.Logs.Add(log);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Log entry created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reservation.");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the reservation. Please try again.");
                Rooms = new SelectList(await _context.Rooms.ToListAsync(), "Id", "RoomName");
                return Page();
            }

            return RedirectToPage("./ReservationList");
        }

    }
}
