using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket_Reservation_System_API.Model;

namespace xunitexample.tests.MockData
{
    public class SearchMockData
    {
        public static List<BusSchedule> result()
        {
            var journeyDate = new DateTime(2025, 11, 15);
            var res= new List<BusSchedule>
            {
                new BusSchedule
                {
                    Id = Guid.NewGuid(),
                    JourneyDate = journeyDate,
                    StartTime = new TimeSpan(8, 0, 0),
                    ArrivalTime = new TimeSpan(14, 0, 0),
                    Price = 1500,
                    Bus = new Bus { CompanyName = "Green Line", BusName = "Green Express" },
                    Route = new Route { From = "Dhaka", To = "Chittagong" },
                    Seats = new List<Seat>
                    {
                        new Seat { Status = SeatStatus.Available },
                        new Seat { Status = SeatStatus.Available },
                        new Seat { Status = SeatStatus.Booked }
                    }
                }
            };
            return res;
        }
    }
}
