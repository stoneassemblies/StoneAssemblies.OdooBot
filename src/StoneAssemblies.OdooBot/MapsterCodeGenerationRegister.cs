
using Mapster;
using StoneAssemblies.OdooBot.Services;

namespace StoneAssemblies.OdooBot;

public class MapsterCodeGenerationRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto")
            .ForAllTypesInNamespace(typeof(ApplicationDbContext).Assembly, "StoneAssemblies.OdooBot.Entities")
            .ExcludeTypes(type => type.IsEnum)
            .IgnoreNullValues(true);
    }
}