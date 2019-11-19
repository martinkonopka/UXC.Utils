using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXC.Core.Data.Serialization.Formats.Csv;
using UXC.Core.Data.Serialization.Formats.Json;
using UXI.Filters;
using UXI.Filters.Configuration;
using UXI.Serialization;
using UXI.Serialization.Extensions;

namespace UXC.Utils.InvalidateZeroPosGazeSamples.Configuration
{
    class UXCDataSerializationFilterConfiguration : FilterConfiguration
    {
        public override void Configure(FilterContext context, object options)
        {
            context.IO
                   .Formats
                   .GetOrDefault(FileFormat.JSON)?
                   .Configurations
                   .Add
                   (
                       new UXCDataJsonConvertersSerializationConfiguration()
                   );

            context.IO
                   .Formats
                   .GetOrDefault(FileFormat.CSV)?
                   .Configurations
                   .Add
                   (
                       new UXCDataCsvConvertersSerializationConfiguration()
                   );
        }
    }
}
