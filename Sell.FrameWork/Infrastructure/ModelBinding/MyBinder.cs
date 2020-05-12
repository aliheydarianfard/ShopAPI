
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sell.Framework.Infrastructure.ModelBinding
{
    public class MyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(string))
            {
                return new BinderTypeModelBinder(typeof(MyBinder));
            }
            return null;
        }

       

    }

    public class MyBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            var valueProvider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var value = valueProvider.FirstValue;

            if (!string.IsNullOrEmpty(value) && value.Contains("Test"))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, " نباید تست باشد ");
                bindingContext.Result = ModelBindingResult.Failed();
            }

            if (!string.IsNullOrEmpty(value))
            {
                bindingContext.Result = ModelBindingResult.Success("Mr " + value);
            }

            return Task.CompletedTask;
        }
    }
}