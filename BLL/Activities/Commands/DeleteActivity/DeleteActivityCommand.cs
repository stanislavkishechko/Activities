using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Activities.Commands.DeleteActivity
{
    public class DeleteActivityCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
