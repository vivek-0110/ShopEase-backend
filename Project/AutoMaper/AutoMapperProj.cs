using AutoMapper;
using Project.Model;
using Project.Model.DTO;

namespace Project.AutoMaper
{
    public class AutoMapperProj : Profile
    {
        public AutoMapperProj()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
        }
    }
}
