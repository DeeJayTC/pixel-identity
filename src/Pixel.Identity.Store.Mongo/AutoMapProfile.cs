﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.MongoDb.Models;
using Pixel.Identity.Shared.ViewModels;

namespace Pixel.Identity.Store.Mongo
{
    /// <summary>
    /// AutoMap profile for mapping required for MongoDb models and pixel identity view models
    /// </summary>
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<ApplicationViewModel, OpenIddictApplicationDescriptor>()
               .ForMember(a => a.DisplayNames, opt => opt.Ignore());

            CreateMap<OpenIddictMongoDbApplication, ApplicationViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
            .ForMember(d => d.IsConfidentialClient, opt => opt.Ignore());

            CreateMap<IdentityUser<Guid>, UserDetailsViewModel>()
             .ForMember(d => d.UserRoles, opt => opt.Ignore())
             .ForMember(d => d.UserClaims, opt => opt.Ignore());

            CreateMap<IdentityRole<Guid>, UserRoleViewModel>()
            .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.RoleName, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Claims, opt => opt.Ignore());

            CreateMap<ScopeViewModel, OpenIddictScopeDescriptor>()
            .ForMember(d => d.DisplayNames, opt => opt.Ignore())
            .ForMember(d => d.Properties, opt => opt.Ignore())
            .ForMember(d => d.Descriptions, opt => opt.Ignore());

            CreateMap<OpenIddictMongoDbScope, ScopeViewModel>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()));
        }
    }
}
