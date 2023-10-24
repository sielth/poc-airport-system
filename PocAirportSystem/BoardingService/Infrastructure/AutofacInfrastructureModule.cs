using System.Reflection;
using Ardalis.SharedKernel;
using Autofac;
using BoardingService.Infrastructure.Checkin;
using BoardingService.Infrastructure.Checkin.Callers;
using BoardingService.Infrastructure.Data;
using Module = Autofac.Module;

namespace BoardingService.Infrastructure;

public class AutofacInfrastructureModule : Module
{
  private readonly bool _isDevelopment = false;
  private readonly List<Assembly> _assemblies = new();
  
  public AutofacInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
  {
    _isDevelopment = isDevelopment;
    AddToAssembliesIfNotNull(callingAssembly);
  }

  private void AddToAssembliesIfNotNull(Assembly? assembly)
  {
    if(assembly != null)
    {
      _assemblies.Add(assembly);
    }
  }
  
  protected override void Load(ContainerBuilder builder)
  {
    LoadAssemblies();
    
    if (_isDevelopment) RegisterDevelopmentOnlyDependencies(builder);
    else RegisterProductionOnlyDependencies(builder);
    
    RegisterEf(builder);
  }
  
  private void LoadAssemblies()
  {
    // TODO: Replace these types with any type in the appropriate assembly/project
    var infrastructureAssembly = Assembly.GetAssembly(typeof(AutofacInfrastructureModule));

    AddToAssembliesIfNotNull(infrastructureAssembly);
  }
  
  private static void RegisterEf(ContainerBuilder containerBuilder)
  {
    containerBuilder.RegisterGeneric(typeof(EfRepository<>))
      .As(typeof(IRepository<>))
      .As(typeof(IReadRepository<>))
      .InstancePerLifetimeScope();
  }
  
  private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
  {
    // NOTE: Add any development only services here
    builder.RegisterType<FakeCheckinCaller>()
      .As<ICheckinCaller>()
      .InstancePerLifetimeScope();
  }

  private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
  {
    // NOTE: Add any production only (real) services here
    builder.RegisterType<CheckinCaller>()
      .As<ICheckinCaller>()
      .InstancePerLifetimeScope();
  }
}