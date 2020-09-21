using MacroContext.ApplicationServices.CommandHandlers;
using MacroContext.Contract.Commands;
using MacroContext.Contract.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.CrossCuttingConcerns
{
    public class EventPublisherCommandProcessorDecorator : ICommandProcessor
    {
        private ICommandProcessor _decorated;
        private EventStoreImpl _eventStore;
        private ICommandHandler<ICommand> _decoratedHandler;
        private lEventPublisher _externalPublisher;
        private bool _suceededFlg = true;
        private string _errorFlg = null;

        public EventPublisherCommandProcessorDecorator(ICommandProcessor decorated, EventStoreImpl eventStore, lEventPublisher externalPublisher)
        {
            _eventStore = eventStore;
            _decorated = decorated;
            _externalPublisher = externalPublisher;
        }

        public void SubmitBatch(params ICommand[] commads)
        {
            try
            {
                _decorated.SubmitBatch(commads);
            }
            catch (Exception e)
            {
                _suceededFlg = false;
                _errorFlg = e.Message;
                _eventStore.ClearEvents();
                throw;
            }
            finally
            {
                var events = _eventStore.GetEventQueue();
                var cmdId = commads.Count() == 1 ? commads.Single().Id : Guid.Empty;
                var eventResult = new TransactionResult(cmdId, events, _suceededFlg, _errorFlg);
                if (_suceededFlg == false || events.Count() != 0) // info to report
                {
                    _externalPublisher.Publish(eventResult);
                    _eventStore.ClearEvents();
                }
            }
        }
    }
}
