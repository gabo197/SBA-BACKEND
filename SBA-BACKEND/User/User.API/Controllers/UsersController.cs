using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using SBA_BACKEND.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using SBA_BACKEND.User.User.API.Services;
using SBA_BACKEND.User.User.API.Services.Communications;
using SBA_BACKEND.User.User.API.Resources;

namespace SBA_BACKEND.User.User.API.Controllers
{
    [Authorize]
    [Route("api/user")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all users",
            Description = "List of Users",
            OperationId = "ListAllUsers",
            Tags = new[] { "users" })]
        [SwaggerResponse(200, "List of Users", typeof(IEnumerable<UserResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await _userService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<User.Domain.AgreggatesModel.User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "List all user emails",
            Description = "List of User emails",
            OperationId = "ListAllUserEmails",
            Tags = new[] { "users" })]
        [SwaggerResponse(200, "List of User Emails", typeof(List<string>))]
        [HttpGet, Route("ListAllEmails")]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        public async Task<List<string>> GetAllEmailsAsync()
        {
            var users = await _userService.ListAllEmailsAsync();
            return users;
        }

        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "users" })]
        [HttpPost]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var user = _mapper.Map<SaveUserResource, User.Domain.AgreggatesModel.User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);
            var userResource = _mapper.Map<User.Domain.AgreggatesModel.User, UserResource>(result.Resource);
            return Ok(userResource);
        }

        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "authentication" })]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _userService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Invalid Username or Password" });

            return Ok(response);
        }

        [SwaggerOperation(Tags = new[] { "users" })]
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int userId, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User.Domain.AgreggatesModel.User>(resource);
            var result = await _userService.UpdateAsync(userId, user);

            if (!result.Success)
                return BadRequest(result.Message);
            var userResource = _mapper.Map<User.Domain.AgreggatesModel.User, UserResource>(result.Resource);
            return Ok(userResource);
        }

        [SwaggerOperation(Tags = new[] { "users" })]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var result = await _userService.GetByIdAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User.Domain.AgreggatesModel.User, UserResource>(result.Resource);

            return Ok(userResource);
        }

        [SwaggerOperation(Tags = new[] { "users" })]
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            var result = await _userService.DeleteAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);
            var userResource = _mapper.Map<User.Domain.AgreggatesModel.User, UserResource>(result.Resource);
            return Ok(userResource);
        }
    }
}
