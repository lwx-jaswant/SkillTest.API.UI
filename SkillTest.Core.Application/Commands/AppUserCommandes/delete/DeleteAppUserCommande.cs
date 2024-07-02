using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SkillTest.Core.Application._DTOs;

namespace SkillTest.Core.Application.Commands.AppUserCommandes.delete
{
    public class DeleteAppUserCommande : IRequest
    {
        public int Id { get; set; }
    }
}
