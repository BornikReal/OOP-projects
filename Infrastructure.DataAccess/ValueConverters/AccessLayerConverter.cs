using Domain.Accounts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class AccessLayerConverter : ValueConverter<AccessLayer, int>
{
    public AccessLayerConverter()
        : base(x => x.Value, x => new AccessLayer(x)) { }
}
