﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Application.Restaurant.Dto;
using Application.Restaurant.ReservationModule.Services;
using Microsoft.AspNet.Identity;
using Restaurant.Host.Helpers;
using Restaurant.Host.Models;
using Swaksoft.Core.Dto;

namespace Restaurant.Host.Controllers
{
    [RoutePrefix("api/reservation")]
    [Authorize]
    public class ReservationController : RestaurantApiController
    {
        private readonly IReservationAppService _reservationAppService;

        public ReservationController(IReservationAppService reservationAppService)
        {
            if (reservationAppService == null) throw new ArgumentNullException("reservationAppService");
            _reservationAppService = reservationAppService;
        }

        // GET api/<controller>
        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = _reservationAppService.GetAllReservations();

            if (result.Status == ActionResultCode.Success)
            {
                return Ok(result.Items);
            }

            return BadRequest(result.Message);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var result = _reservationAppService.GetReservation(id);

            if (result.Status == ActionResultCode.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        // POST api/<controller>
        [ResponseType(typeof(ReservationResult))]
        public IHttpActionResult Post([FromBody]ReservationViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservationDate = viewModel.ReservationDate;
            var reservationTime = viewModel.ReservationTime;

            //creates a new request for a reservation
            var request = new ReservationRequest
            {
                UserId = User.Identity.GetUserId(),
                ReservationDateTime = new DateTime(
                    reservationDate.Year,
                    reservationDate.Month,
                    reservationDate.Day,
                    reservationTime.Hour,
                    reservationTime.Minute,
                    0,
                    DateTimeKind.Utc),
                GuestsCount = viewModel.GuestsCount,
                Name = viewModel.Name
            };

            //saves this reservation to the database
            var result = _reservationAppService.AddNewReservation(request);

            if (result.Status == ActionResultCode.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]ReservationViewModel viewModel)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_reservationAppService != null)
                {
                    _reservationAppService.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}