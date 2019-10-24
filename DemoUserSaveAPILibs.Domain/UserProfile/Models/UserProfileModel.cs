using DemoUserSaveAPILibs.Core.Data.Domain;
using System;

namespace DemoUserSaveAPILibs.Domain.UserProfile.Models
{
    public class UserProfileModel : IDomainModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
