using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Models;
using Simbir.GO.Repositories.Interfaces;

namespace Simbir.GO.Controllers;

[Produces("application/json")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;

    public AccountController(ILogger<AccountController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    /// <summary>
    /// получение данных о текущем аккаунте
    /// </summary>
    /// <response code="200">Получение данных о текущем аккаунте.</response>
    /// <response code="401">Пользователь не авторизорван</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpGet, Authorize]
    [Route("/Me")]
    [ProducesResponseType(typeof(UserViewModel), 200)]
    public IActionResult Me()
    {
        return new JsonResult("jija");
    }

    /// <summary>
    /// получение нового jwt токена пользователя
    /// </summary>  
    /// <param name="body"></param>
    /// <response code="200">Пользователь успешно авторизовался</response>
    /// <response code="401">Данные не верны или отсутствуют</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPost]
    [Route("/SignIn")]
    [ProducesResponseType(typeof(TokenModel), 200)]
    public IActionResult SignIn(LoginModel userLogin)
    {
        var response = _userService.Authenticate(userLogin);
        return StatusCode(response.StatusCode, response.Result);
    }

    /// <summary>
    /// выход из аккаунта
    /// </summary>
    /// <response code="200">Пользователь успешно вышел из аккаунта</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPost, Authorize]
    [Route("/SignOut")]
    public IActionResult SignOut()
    {
        return new JsonResult(null);
    }

    /// <summary>
    /// регистрация нового аккаунта
    /// </summary>
    /// <param name="body"></param>
    /// <response code="200">Пользователь успешно зарегистрировался</response>
    /// <response code="403">Такой пользователь уже существует </response>
    /// <response code="400">Данные некорректны</response>
    /// <response code="500">Непредвиденная ошибка</response>
    [HttpPost]
    [Route("/SignUp")]
    public IActionResult SignUp(RegisterModel registerModel)
    {
        if (!ModelState.IsValid) return StatusCode(400, "Данные некорректны");
        _userService.Create(registerModel.Name, registerModel.Password);

        return Ok("Пользователь успешно зарегистрировался");
    }

    /// <summary>
    /// обновление своего аккаунта
    /// </summary>
    /// <param name="body"></param>
    /// <response code="200">Данные успешно обновлены</response>
    /// <response code="401">Пользователь не авторизован</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPut, Authorize]
    [Route("/Update")]
    [ProducesResponseType(typeof(LoginModel), 200)]
    public IActionResult Update(LoginModel userLogin)
    {
        return new JsonResult(null);
    }
}