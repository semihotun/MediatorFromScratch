using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorFromScratch.Mediator
{
    public static class MediatorServiceCollectionExtension
    {
        public  static IServiceCollection AddMediator(this IServiceCollection services,ServiceLifetime lifetime,params Type[] markers)
        {
            var handlerInfo = new Dictionary<Type, Type>();

            foreach(var marker in markers)
            {
                var assembly = marker.Assembly;
                var requests=GetClassesImplementingInterface(assembly, typeof(IRequest<>));
                var handlers= GetClassesImplementingInterface(assembly, typeof(IHandler<,>));

                var deneq = handlers[0].GetInterface("IHandler`2");

                // CommandExampleRequestler aynıysa
                requests.ForEach(x =>
                {
                    handlerInfo[x] = handlers.SingleOrDefault(y =>x == y.GetInterface("IHandler`2")!.GetGenericArguments()[0]);
                });

                //Handler'ları scope olara ekle
                var serviceDescriptopr = handlers.Select(x => new ServiceDescriptor(x, x, lifetime));
                services.TryAdd(serviceDescriptopr); 
            }

            IServiceProvider spb = services.BuildServiceProvider();
            services.AddSingleton<IMediator>(x => new Mediator(spb.GetRequiredService, handlerInfo));
            return services; 
        }

        private static List<Type> GetClassesImplementingInterface(System.Reflection.Assembly assembly,Type typeToMatch)
        {
            return assembly.ExportedTypes.Where(type =>
            {
                var genericInterfaceTypes = type.GetInterfaces().Where(x => x.IsGenericType).ToList();
                var implementRequestType = genericInterfaceTypes.Any(x => x.GetGenericTypeDefinition() == typeToMatch);
                return !type.IsInterface && !type.IsAbstract && implementRequestType;
            }).ToList();
        }
    }
}
