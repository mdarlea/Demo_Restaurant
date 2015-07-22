﻿using System;
using Swaksoft.Core.Dto;

namespace Application.Restaurant.Dto
{
    public class ReservationResult : ActionResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ReservationDateTime { get; set; }

        public int GuestsCount { get; set; }
    }
}
