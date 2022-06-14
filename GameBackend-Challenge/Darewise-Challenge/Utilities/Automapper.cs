using AutoMapper;
using Darewise_Challenge.DTOs;
using GameBackend_Challenge.Entities;

namespace Darewise_Challenge.Utilities
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<BugCreationDTO, Bug>()
            .ForMember(x => x.Attachment, options => options.Ignore());


            CreateMap<PlayerCreationDTO, Player>();

            CreateMap<Player, PlayerCreationDTO>();
        }

    }
}
