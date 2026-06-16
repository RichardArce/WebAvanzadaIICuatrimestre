using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAvanzadaIICuatrimestre.BLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            // Map Carro entity to CarroDto and map the navigation property FkduennoNavigation -> Duenno
            CreateMap<DAL.Entidades.Carro, Dtos.CarroDto>()
                .ForMember(dest => dest.Duenno, opt => opt.MapFrom(src => src.FkduennoNavigation))
                .ReverseMap()
                .ForMember(dest => dest.Fkduenno, opt => opt.MapFrom(src => src.Duenno != null ? src.Duenno.Id : src.Fkduenno));
            CreateMap<DAL.Entidades.Duenno, Dtos.DuennoDto>().ReverseMap();
            CreateMap<DAL.Entidades.Telefono, Dtos.TelefonoDto>().ReverseMap();
        }
    }

    //MAPPER
}
