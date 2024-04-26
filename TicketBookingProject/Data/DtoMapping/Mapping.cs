using AutoMapper;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.Data.Models;
using TicketBookingProject.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PassengerDto, PassengerDetails>();
        CreateMap<UserCredentialsDto, UsersCredential>();
        CreateMap<TrainDetails, TrainDetailsDto>();
        CreateMap<TrainWiseSeatAvailability, TrainWiseSeatAvailablityDto>();
        // Add mappings for other entities and DTOs as needed
    }
}
