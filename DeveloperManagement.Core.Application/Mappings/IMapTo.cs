using AutoMapper;

namespace DeveloperManagement.Core.Application.Mappings
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}