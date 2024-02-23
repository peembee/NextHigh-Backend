﻿namespace GoApptechBackend.Models.DTO.PersonDTO
{
    public class GetPersonDTO
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int EmpPoints { get; set; }

        public int PongPoints { get; set; }

        public double YearsInPratice { get; set; }

        public int LossesInPingPong { get; set; }

        public int WinningsInPingPong { get; set; }
    }
}
