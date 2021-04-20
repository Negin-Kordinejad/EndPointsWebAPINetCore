using AutoMapper;
using ClientWebAPI.Models;
using EndPointsWebAPINetCore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Journy, JournyDto>();
            CreateMap<Listing, ListingDto>();
        }
    }
}
