using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace CustomStringLocalizerSample.Web.Localization
{
    public class LocalizedValidationMetadataProvider : IValidationMetadataProvider
    {
        private IStringLocalizer _localizer;
        public LocalizedValidationMetadataProvider(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            if (context.Key.MetadataKind == ModelMetadataKind.Property && context.PropertyAttributes.Count > 0)
            {
                foreach (var attr in context.PropertyAttributes)
                    if (attr is ValidationAttribute)
                        context.ValidationMetadata.ValidatorMetadata.Add(attr);

                foreach (var metadata in context.ValidationMetadata.ValidatorMetadata)
                {
                    if (metadata is ValidationAttribute attribute)
                    {
                        attribute.ErrorMessage = _localizer[attribute.ErrorMessage].Value;
                    }
                }
            }
        }
    }
}
