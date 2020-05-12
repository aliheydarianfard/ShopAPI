
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sell.Core.Extension;

namespace Sell.Framwork.Infrastructure.ModelBinders
{
    public class PersianDateEntityBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }


            if (context.Metadata.ModelType == typeof(DateTime?) || context.Metadata.ModelType == typeof(DateTime))
            {
                return new BinderTypeModelBinder(typeof(PersianDateModelBinder));
            }
            return null;
        }
    }

    public class PersianDateModelBinder : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            var valueProviderResult =
               bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            string date = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(date) || date.Length != 10)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, " فرمت تاریخ صحیح نیست ");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
      

            if (bindingContext.ModelType == typeof(DateTime))
                bindingContext.Result = ModelBindingResult.Success(date.PersianToDateTime());

            if (bindingContext.ModelType == typeof(DateTime?))
                bindingContext.Result = ModelBindingResult.Success(new DateTime?(date.PersianToDateTime()));


            return Task.CompletedTask;
        }

    }
}