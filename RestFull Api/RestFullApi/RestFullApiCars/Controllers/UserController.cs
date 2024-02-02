using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models;
using EntityFramework.Entities;
using EntityFramework.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XAct;

namespace RestFullApiCars.Controllers;

[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public UserController(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	[ResponseCache(CacheProfileName = "PorDefecto20seg")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetAllUser()
	{
		var users = await _userRepository.GetAllUsersAsync();

		return Ok(users.ToModel());
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("{userId:int}", Name = "GetUser")]
	[ResponseCache(CacheProfileName = "PorDefecto20seg")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetUser(int userId)
	{
		var user = await _userRepository.GetUserAsync(userId);

		if(user == null)
		{
			return NotFound();
		}

		return Ok(user.ToModel());
	}

	[HttpPost("Register")]
	[ProducesResponseType(201, Type = typeof(CreateUserDto))]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
	{
		if (await _userRepository.UserExistsAsync(null, createUserDto.NameUser))
		{
			ModelState.AddModelError("Error", "Ya existe una cuenta con este nombre de usuario");
			return StatusCode(404, ModelState);
		}

		if (!ModelState.IsValid || createUserDto == null)
		{
			return BadRequest(ModelState);
		}

		var grabo = await _userRepository.RegisterUserAsync(createUserDto.ToEntity());

		if (grabo == null)
		{
			ModelState.AddModelError("Error", $"Algo salió mal al crear el registro {createUserDto.NameUser}");
			return StatusCode(500, ModelState);
		}

		return Ok("Registro creado correctamente");
	}

	[HttpPost("Login")]
	[ProducesResponseType(201, Type = typeof(LoginUserDto))]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
	{
		if (!await _userRepository.UserExistsAsync(null, loginUserDto.NameUser))
		{
			ModelState.AddModelError("Error", "No existe una cuenta con este nombre de usuario");
			return StatusCode(404, ModelState);
		}

		if (!ModelState.IsValid || loginUserDto == null)
		{
			return BadRequest(ModelState);
		}

		(var token, User user) = await _userRepository.LoginAsync(loginUserDto.NameUser, loginUserDto.Password);

		if (string.IsNullOrEmpty(token))
		{
			ModelState.AddModelError("Error", $"El nombre de usuario o contraseña son incorrectos");
			return StatusCode(500, ModelState);
		}

		return Ok($"Token: {token}");
	}
}
