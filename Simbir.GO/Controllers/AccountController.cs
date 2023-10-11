using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Models;

namespace Simbir.GO.Controllers;

[Produces("application/json")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// получение данных о текущем аккаунте
    /// </summary>
    /// <response code="200">Получение данных о текущем аккаунте.</response>
    /// <response code="401">Пользователь не авторизорван</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpGet]
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
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPost]
    [Route("/SignIn")]
    [ProducesResponseType(typeof(TokenModel), 200)]
    public IActionResult SignIn(LoginModel userLogin)
    {
        return new JsonResult(null);
    }

    /// <summary>
    /// выход из аккаунта
    /// </summary>
    /// <response code="200">Пользователь успешно вышел из аккаунта</response>
    /// <response code="403">Пользователь не авторизован</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPost]
    [Route("/SignOut")]
    public IActionResult SignOut()
    {
        return new JsonResult(null);
    }

    /// <summary>
    /// регистрация нового аккаунта
    /// </summary>
    /// <param name="body"></param>
    /// <response code="201">Пользователь успешно зарегистрировался</response>
    /// <response code="401">Такой пользователь уже существует или данные не верны</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPost]
    [Route("/SignUp")]
    [ProducesResponseType(typeof(TokenModel), 201)]
    public IActionResult SignUp(RegisterModel registerModel)
    {
        return new JsonResult(null);
    }

    /// <summary>
    /// обновление своего аккаунта
    /// </summary>
    /// <param name="body"></param>
    /// <response code="200">Данные успешно обновлены</response>
    /// <response code="403">Пользователь не авторизован</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPut]
    [Route("/Update")]
    [ProducesResponseType(typeof(LoginModel), 200)]
    public IActionResult Update(LoginModel userLogin)
    {
        return new JsonResult(null);
    }
}