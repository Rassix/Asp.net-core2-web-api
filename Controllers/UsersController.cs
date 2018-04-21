using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Exceptions;
using DatingApp.API.Models;
using DatingApp.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Edm.Library.Expressions;

namespace DatingApp.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IDatingRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _userRepository = repo;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(IList<User>), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(401)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null) return NotFound("User not found");

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
                
            return Ok(userToReturn);
        }

        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUser = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _userRepository.GetUser(id);

            if (userFromRepo == null || userFromRepo.Id != currentUser)
                return Unauthorized();

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _userRepository.SaveAll())
                return NoContent();

            throw new UserUpdateException($"Updating user {id} failed on save");
        }
    }
}