using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorFromScratch.Mediator
{
    public class Mediator : IMediator
    {
        //Gönderilen servisler
        private readonly Func<Type, object> _serviceResolver;

        //ConcurrentDictonary dictionary'den farkı Async olarak  aynı key değerlerinin üretilmesini engeller
        private readonly IDictionary<Type, Type> _handlerDetails;

        public Mediator(Func<Type, object> serviceResolver, IDictionary<Type, Type> handlerDetails)
        {
            _serviceResolver = serviceResolver;
            _handlerDetails = handlerDetails;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            if(!_handlerDetails.ContainsKey(requestType))
            {
                throw new Exception($"No handler request of type {requestType.Name}");
            }
            _handlerDetails.TryGetValue(requestType, out var requestHandlerType);
            //Servislerden request'e gelen servis
            var handler = _serviceResolver(requestHandlerType);

            //HandleAsync methoduna gelen parametreleri gönder
            return await (Task<TResponse>)handler.GetType().GetMethod("HandleAsync").Invoke(handler, new[] { request });

 
        }
    }
}
