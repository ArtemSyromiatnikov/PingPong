﻿namespace PingPong.Sdk.Models.Players
{
    public class PlayerInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Total { get; set; }
    }
}