// using Autofac;
// using DeveloperManagement.Core.Application.Interfaces;
// using DeveloperManagement.Core.Domain.Interfaces;
// using DeveloperManagement.WorkItemManagement.Application.Interfaces;
// using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
// using DeveloperManagement.WorkItemManagement.Infrastructure.MimeType;
// using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence;
// using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
// using DeveloperManagement.WorkItemManagement.Infrastructure.Services;
//
// namespace DeveloperManagement.WorkItemManagement.WebApi.StartupModules.Infrastructure.AutofacModules
// {
//     public class ApplicationModule : Module
//     {
//         private string _connectionString;
//
//         public ApplicationModule(string connectionString)
//         {
//             _connectionString = connectionString;
//         }
//
//         protected override void Load(ContainerBuilder builder)
//         {
//             builder.Register(c => new DapperConnectionFactory(_connectionString))
//                 .As<IDapperConnectionFactory>().SingleInstance();
//
//             builder.Register(c => new UnitOfWork(c.Resolve<IDapperConnectionFactory>(),
//                 c.Resolve<IDomainEventService>(), c.Resolve<ICurrentUserService>(),
//                 c.Resolve<IDateTime>())).As<IUnitOfWork>().InstancePerLifetimeScope();
//
//             builder.RegisterType<DomainEventService>().As<IDomainEventService>().InstancePerLifetimeScope();
//
//             builder.RegisterType<DateTimeService>().As<IDateTime>().InstancePerDependency();
//
//             builder.RegisterType<MimeTypeMapper>().As<IMimeTypeMapper>().SingleInstance();
//         }
//     }
// }