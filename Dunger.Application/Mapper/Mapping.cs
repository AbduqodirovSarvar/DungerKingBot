using AutoMapper;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.Filials.Commands;
using Dunger.Application.UseCases.Menus.Commands;
using Dunger.Domain.Entities;

namespace Dunger.Application.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateMenuCommand, Menu>().ReverseMap();
            CreateMap<Menu, MenuViewModel>()
                .ForMember(x => x.Photo, y => y.MapFrom(z => $"{z.PhotoId}"));
            CreateMap<FilialCreateCommand, Filial>().ReverseMap();
            CreateMap<Filial, FilialViewModel>()
                .ForMember(x => x.Menus, y => y.MapFrom(z => z.Menus))
                .ForMember(x => x.Orders, y => y.MapFrom(z => z.Orders));

        }
    }
}
