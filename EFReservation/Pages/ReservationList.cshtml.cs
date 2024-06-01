using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFReservation.Models;

namespace EFReservation.Pages
{
    public class ReservationListModel : PageModel
    {
        private readonly AppDbContext _context;

        public ReservationListModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservations { get; set; }

        public async Task OnGetAsync(string roomName, DateTime? startDate, DateTime? endDate, int? capacity)
        {
            var query = _context.Reservations.Include(r => r.Room).AsQueryable();

            if (!string.IsNullOrEmpty(roomName))
            {
                query = query.Where(r => r.Room.RoomName.Contains(roomName));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(r => r.Date >= startDate.Value && r.Date <= endDate.Value);
            }

            if (capacity.HasValue)
            {
                query = query.Where(r => r.Room.Capacity >= capacity.Value);
            }

            Reservations = await query.ToListAsync();
        }
    }
}
