namespace TicketBookingProject.Data.Models
{
    public class PassengerRequestModel
    {
        public string Passenger_Name { get; set; }
        public int Passenger_Age { get; set; }
        public string Passenger_gender { get; set; }
        public int TrainNumber { get; set; }
        //public string StartingCity { get; set; }
        //public string DestinationCity { get; set; }
        public string Class { get; set; }
        public int TotalTicketCount { get; set; }
        //public int TotalFare { get; set; }
        //public string Status { get; set; }

    }
}
