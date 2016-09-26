﻿/*
 *     Copyright 2016 Adam Burton (adz21c@gmail.com)
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using MassTransit;
using MassTransit.ConsumeConfigurators;
using Microsoft.Practices.ServiceLocation;
using Miles.MassTransit.MessageDeduplication;
using Miles.Persistence;

namespace Miles.MassTransit.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class MessageDeduplicationExtensions
    {
        /// <summary>
        /// Wraps message in a <see cref="ITransactionContext"/> in which the message is recorded to ensure it is processed only once.
        /// On identifying a message as already processed the message is removed from the queue without doing any work.
        /// </summary>
        /// <remarks>
        /// This assumes a container will have registered itself as an <see cref="IServiceLocator"/> payload to 
        /// retrieve an <see cref="IConsumedRepository"/> instance that will work with the <see cref="ITransactionContext"/>.
        /// </remarks>
        /// <typeparam name="TConsumer">The type of the consumer.</typeparam>
        /// <param name="configurator">The configurator.</param>
        /// <returns></returns>
        public static IConsumerConfigurator<TConsumer> UseMessageDeduplication<TConsumer>(this IConsumerConfigurator<TConsumer> configurator)
            where TConsumer : class, IConsumer
        {
            // Transaction is required to ensure recording and
            // processing of message are a single unit of work
            configurator.UseTransactionContext();

            var spec = new MessageDeduplicationSpecification<TConsumer>();
            configurator.AddPipeSpecification(spec);
            return configurator;
        }
    }
}
