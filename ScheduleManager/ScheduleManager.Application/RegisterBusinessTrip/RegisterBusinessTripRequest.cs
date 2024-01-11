using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ScheduleManager.Domain.RegisterBusinessTrip;

namespace ScheduleManager.Application.RegisterBusinessTrip
{
    public class RegisterBusinessTripRequest : IRequest<bool>
    {
        public string Email { get; set; }

        public RegisterBusinessTripModel RegisterBusinessTripModel { get; set; }
    }
}
