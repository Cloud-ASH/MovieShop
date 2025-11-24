using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

public class CastController : Controller
{
    private readonly ICastService _castService;

    public CastController(ICastService castService)
    {
        _castService = castService;
    }

    // GET: Cast
    public IActionResult Index()
    {
        return View();
    }

    // GET: Cast/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var cast = await _castService.GetCastDetails(id);
        if (cast == null)
        {
            return NotFound();
        }
        return View(cast);
    }
}
