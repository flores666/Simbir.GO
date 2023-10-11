using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Simbir.GO.Controllers;

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
    public IActionResult Me()
    {
        return new JsonResult(null);
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
    public IActionResult SignIn()
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
    /// <response code="200">Пользователь успешно зарегистрировался</response>
    /// <response code="401">Такой пользователь уже существует или данные не верны</response>
    /// <response code="0">Непредвиденная ошибка</response>
    [HttpPost]
    [Route("/SignUp")]
    public IActionResult SignUp()
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
    public IActionResult Update()
    {
        return new JsonResult(null);
    }
}