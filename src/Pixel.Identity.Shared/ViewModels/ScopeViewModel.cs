﻿using AutoMapper;
using OpenIddict.Abstractions;
using OpenIddict.MongoDb.Models;
using System.Collections.Generic;

namespace Pixel.Identity.Shared.ViewModels
{
    [AutoMap(typeof(OpenIddictScopeDescriptor))]
    [AutoMap(typeof(OpenIddictMongoDbScope))]
    public class ScopeViewModel
    {
        /// <summary>
        /// Identifier of the scope
        /// </summary>
        [IgnoreMap]
        public string Id { get; set; }

        private string name;
        /// <summary>
        /// Name of the scope
        /// </summary>
        public string Name
        {
            get => name ?? string.Empty;          
            set => name = value;
        }

        /// <summary>
        /// Display name of the scope
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the scope
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Resources associated with the scope.
        /// Resources are added on claims prinicipal
        /// </summary>
        public List<string> Resources { get; set; } = new();

    }
}
