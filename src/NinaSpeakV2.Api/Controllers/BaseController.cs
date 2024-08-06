using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;

namespace NinaSpeakV2.Api.Controllers
{
    public abstract class BaseController<TEntity, TCreateViewModel, TUpdateViewModel, TReadViewModel> : 
                          BaseReadonlyController<TEntity, TReadViewModel>
        where TEntity          : class, IBaseEntityGlobal
        where TCreateViewModel : class, IBaseCreateViewModel
        where TUpdateViewModel : class, IBaseUpdateViewModel
        where TReadViewModel   : class, IBaseReadViewModel, new()
    {
        protected IBaseService<TEntity, TCreateViewModel, TUpdateViewModel, TReadViewModel> _baseService;

        protected BaseController(IBaseService<TEntity, TCreateViewModel, TUpdateViewModel, TReadViewModel> baseService, IMapper mapper)
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
        public virtual async Task<IActionResult> Create(TCreateViewModel createModel)
        {
            var value = await _baseService.CreateAsync(createModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                return View(createModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Edit/{id?}")]
        public virtual async Task<IActionResult> Edit(long? id)
        {            
            var entity = await _readonlyService.GetByIdAsync(id ?? 0);

            if (!BaseValidator.IsValid(entity))
                return NotFound();

            var updateModel = _mapper.Map<TUpdateViewModel>(entity);
            return View(updateModel);
        }

        [HttpPost("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(long id, TUpdateViewModel updateModel)
        {
            if (id != updateModel.Id)
                return BadRequest();

            var value = await _baseService.UpdateAsync(updateModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                return View(updateModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Delete/{id?}")]
        public virtual async Task<IActionResult> Delete(long? id)
        {            
            var value = await _readonlyService.GetByIdAsync(id ?? 0);

            if (!BaseValidator.IsValid(value))
                return NotFound(); //Return UnprocessableEntity ?

            return View(value);
        }

        [HttpPost("Delete/{id}"), ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(long id)
        {
            if (!await _baseService.SoftDeleteAsync(id))
                return NotFound(); //Return UnprocessableEntity ?

            return RedirectToAction("Index", "Home");
        }
    }
}
