using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerShop.DataStore.Test
{
    public abstract class EntityTest
    {
        protected IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
