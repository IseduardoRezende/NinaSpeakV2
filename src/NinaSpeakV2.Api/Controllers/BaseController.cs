using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Services.IServices;

namespace NinaSpeakV2.Api.Controllers
{
    public abstract class BaseController<TModel, CreateModel, UpdateModel, ReadModel> : BaseReadonlyController<TModel, ReadModel>
        where TModel      : class, IBaseModelGlobal
        where CreateModel : class, IBaseCreateViewModel
        where UpdateModel : class, IBaseUpdateViewModel
        where ReadModel   : class, IBaseReadViewModel, new()
    {
        protected IBaseService<TModel, CreateModel, UpdateModel, ReadModel> _baseService;

        protected BaseController(IBaseService<TModel, CreateModel, UpdateModel, ReadModel> baseService, IMapper mapper)
            : base(baseService, mapper)
        {
            _baseService = baseService;
        }

        [HttpGet("Create")]
        public virtual async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        [HttpPost("Create"), ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(CreateModel createModel)
        {
            var value = await _baseService.CreateAsync(createModel);

            if (value.HasErrors())
            {
                ViewData = ViewData.SetBaseErrors(value.BaseErrors!);
                return View(createModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Edit/{id?}")]
        public virtual async Task<IActionResult> Edit(long? id)
        {            
            var model = await _readonlyService.GetByIdAsync(id ?? 0);

            if (model is null)
                return NotFound();

            var updateModel = _mapper.Map<UpdateModel>(model);
            return View(updateModel);
        }

        [HttpPost("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(long id, UpdateModel updateModel)
        {
            if (id != updateModel.Id)
                return BadRequest();

            var value = await _baseService.UpdateAsync(updateModel);

            if (value.HasErrors())
            {
                ViewData = ViewData.SetBaseErrors(value.BaseErrors!);
                return View(updateModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Delete/{id?}")]
        public virtual async Task<IActionResult> Delete(long? id)
        {            
            var value = await _readonlyService.GetByIdAsync(id ?? 0);

            if (value is null)
                return NotFound();

            return View(value);
        }

        [HttpPost("Delete/{id}"), ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(long id)
        {
            if (!await _baseService.SoftDeleteAsync(id))
                return NotFound();

            return RedirectToAction("Index", "Home");
        }
    }
}
