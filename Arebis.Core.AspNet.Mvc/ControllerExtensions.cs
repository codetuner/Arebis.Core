using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Arebis.Core.AspNet.Mvc
{
    /// <summary>
    /// Controller extension methods.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Binds the request to a model of the given type T.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="controller">The controller handling the request.</param>
        /// <param name="prefix">Optional prefix of the model.</param>
        /// <param name="initialize">Optional model initilizer.</param>
        /// <returns>The bound model.</returns>
        /// <example>
        /// <code lang="csharp">
        /// public async Task&lt;IActionResult&gt; Submit()
        /// {
        ///     var model = await this.BindModelAsync&lt;MyModel&gt;();
        ///     ...
        ///     // Optionally validate the model:
        ///     TryValidateModel(model);
        ///     if (ModelState.IsValid)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static async Task<T> BindModelAsync<T>(this Controller controller, string? prefix = null, Action<T>? initialize = null)
            where T : class
        {
            var httpContext = controller.HttpContext;
            var services = httpContext.RequestServices;

            var modelMetadataProvider = services.GetRequiredService<IModelMetadataProvider>();
            var modelBinderFactory = services.GetRequiredService<IModelBinderFactory>();
            var modelMetadata = modelMetadataProvider.GetMetadataForType(typeof(T));

            var modelBinder = modelBinderFactory.CreateBinder(new ModelBinderFactoryContext
            {
                BindingInfo = new BindingInfo(),
                Metadata = modelMetadata,
                CacheToken = typeof(T)
            });

            var modelState = controller.ModelState;
            var valueProvider = await CompositeValueProvider.CreateAsync(controller.ControllerContext);

            var bindingContext = DefaultModelBindingContext.CreateBindingContext(
                controller.ControllerContext,
                valueProvider,
                modelMetadata,
                bindingInfo: null,
                modelName: prefix ?? string.Empty // You can customize if needed
            );

            await modelBinder.BindModelAsync(bindingContext);

            if (bindingContext.Result.IsModelSet)
            {
                var result = (T)(bindingContext.Result.Model ?? Activator.CreateInstance<T>());
                initialize?.Invoke(result);
                return result;
            }
            else
            { 
                var result = Activator.CreateInstance<T>();
                initialize?.Invoke(result);
                return result;
            }
        }

        /// <summary>
        /// Binds the request to a model of the given type T.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="controller">The controller handling the request.</param>
        /// <param name="model">The model to bind.</param>
        /// <param name="prefix">Optional prefix of the model.</param>
        /// <exception cref="ArgumentNullException">Raised if no model is given.</exception>
        /// <example>
        /// <code lang="csharp">
        /// public async Task&lt;IActionResult&gt; Submit()
        /// {
        ///     var model = new MyModel();
        ///     await this.BindModelAsync(model);
        ///     ...
        ///     // Optionally validate the model:
        ///     TryValidateModel(model);
        ///     if (ModelState.IsValid)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public static async Task BindModelAsync<T>(this Controller controller, T model, string? prefix = null)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var httpContext = controller.HttpContext;
            var services = httpContext.RequestServices;

            var metadataProvider = services.GetRequiredService<IModelMetadataProvider>();
            var binderFactory = services.GetRequiredService<IModelBinderFactory>();

            var modelMetadata = metadataProvider.GetMetadataForType(typeof(T));
            var modelBinder = binderFactory.CreateBinder(new ModelBinderFactoryContext
            {
                BindingInfo = new BindingInfo(),
                Metadata = modelMetadata,
                CacheToken = typeof(T)
            });

            var valueProvider = await CompositeValueProvider.CreateAsync(controller.ControllerContext);

            var bindingContext = DefaultModelBindingContext.CreateBindingContext(
                controller.ControllerContext,
                valueProvider,
                modelMetadata,
                bindingInfo: null,
                modelName: prefix ?? string.Empty
            );

            bindingContext.Model = model;
            bindingContext.ModelState = controller.ModelState;
            bindingContext.ValidationState.Add(model, new ValidationStateEntry { Metadata = modelMetadata });

            await modelBinder.BindModelAsync(bindingContext);
        }
    }
}