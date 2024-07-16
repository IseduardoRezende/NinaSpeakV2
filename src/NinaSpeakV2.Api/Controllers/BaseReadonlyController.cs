using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NinaSpeakV2.Domain.Services.IServices;
using Microsoft.AspNetCore.RateLimiting;
using NinaSpeakV2.Api.Enums;

namespace NinaSpeakV2.Api.Controllers
{
    [Route("[controller]"), Authorize, EnableRateLimiting(nameof(PolicyType.Authenticated))]
    public abstract class BaseReadonlyController<TModel, ReadModel> : Controller
        where TModel    : class, IBaseModelGlobal
        where ReadModel : class, IBaseReadViewModel, new()
    {
        //ADD CANCELLETIONTOKEN ON EACH METHOD

        protected IBaseReadonlyService<TModel, ReadModel> _readonlyService;
        protected IMapper _mapper;

        protected BaseReadonlyController(IBaseReadonlyService<TModel, ReadModel> readonlyService, IMapper mapper)
        {
            _readonlyService = readonlyService;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index()
        {
            return View(await _readonlyService.GetAsync());
        }

        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> Details(long? id)
        {
           return View(await _readonlyService.GetByIdAsync(id ?? 0));      
        }        
    }
}
