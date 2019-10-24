using DemoUserSaveAPI.Models.UserProfile;
using DemoUserSaveAPILibs.Domain.UserProfile.Models;
using DemoUserSaveAPILibs.Domain.UserProfile.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoUserSaveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userProfileService.Get();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostUserProfileModel userProfileModel)
        {
            var mappedModel = new UserProfileModel
            {
                Username = userProfileModel.Username
            };

            var userId = await _userProfileService.Save(mappedModel);
            return Ok(userId);
        }
    }
}