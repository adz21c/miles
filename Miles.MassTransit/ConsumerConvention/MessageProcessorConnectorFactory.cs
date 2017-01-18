﻿// Copyright 2007-2016 Adam Burton, Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
using MassTransit.ConsumeConnectors;
using Miles.Messaging;
using System;

namespace Miles.MassTransit.ConsumerConvention
{
    class MessageProcessorConnectorFactory<TProcessor, TMessage> : IMessageConnectorFactory
        where TProcessor : class, IMessageProcessor<TMessage>
        where TMessage : class
    {
        private readonly MessageProcessorMessageFilter<TProcessor, TMessage> filter = new MessageProcessorMessageFilter<TProcessor, TMessage>();

        public IConsumerMessageConnector<T> CreateConsumerConnector<T>() where T : class
        {
            var result = new ConsumerMessageConnector<TProcessor, TMessage>(filter) as IConsumerMessageConnector<T>;
            if (result == null)
                throw new ArgumentException("The consumer type did not match the connector type");

            return result;
        }

        public IInstanceMessageConnector<T> CreateInstanceConnector<T>() where T : class
        {
            var result = new InstanceMessageConnector<TProcessor, TMessage>(filter) as IInstanceMessageConnector<T>;
            if (result == null)
                throw new ArgumentException("The consumer type did not match the connector type");

            return result;
        }
    }
}