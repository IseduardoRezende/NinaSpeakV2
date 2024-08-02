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
    public abstract class BaseReadonlyController<TEntity, TReadViewModel> : Controller
        where TEntity        : class, IBaseEntityGlobal
        where TReadViewModel : class, IBaseReadViewModel, new()
    {
        //ADD CANCELLETIONTOKEN ON EACH METHOD

        protected IBaseReadonlyService<TEntity, TReadViewModel> _readonlyService;
        protected IMapper _mapper;

        protected BaseReadonlyController(IBaseReadonlyService<TEntity, TReadViewModel> readonlyService, IMapper mapper)
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
