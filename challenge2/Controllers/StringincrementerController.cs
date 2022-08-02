using System.Text.RegularExpressions;
using challenge2.Biz;
using Microsoft.AspNetCore.Mvc;

namespace challenge2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StringincrementerController : ControllerBase
{
    private readonly ILogger<StringincrementerController> _logger;
    private readonly IStringincrementerService _stringcrementerService;
    public StringincrementerController(ILogger<StringincrementerController> logger, IStringincrementerService stringincrementerService)
    {
        _logger = logger;
        _stringcrementerService = stringincrementerService;
    }
    [HttpGet("{inputString}")]
    public IActionResult Stringincrementer(string inputString)
    {
        string response = _stringcrementerService.CalculateIncreaseFromString(inputString);
        return Ok(response);
    }
}
