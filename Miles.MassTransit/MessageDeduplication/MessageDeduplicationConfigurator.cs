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
using Miles.MassTransit.Configuration;
using Miles.Messaging;

namespace Miles.MassTransit.MessageDeduplication
{
    class MessageDeduplicationConfigurator : IMessageDeduplicationConfigurator
    {
        public MessageDeduplicationConfigurator(QueueNameAttribute queueAttrib = null, MessageDeduplicationAttribute attrib = null)
        {
            if (attrib != null)
                Enabled = attrib.Enabled;

            if (queueAttrib != null)
                QueueName = queueAttrib.QueueName;
        }

        public bool Enabled { get; private set; } = true;

        IMessageDeduplicationConfigurator IMessageDeduplicationConfigurator.Enable(bool enable)
        {
            this.Enabled = enable;
            return this;
        }

        public string QueueName { get; private set; }

        IMessageDeduplicationConfigurator IMessageDeduplicationConfigurator.QueueName(string queueName)
        {
            this.QueueName = queueName;
            return this;
        }

        public MessageDeduplicationSpecification<TContext> CreateSpecification<TContext>()
            where TContext : class, ConsumeContext
        {
            return new MessageDeduplicationSpecification<TContext>(this);
        }
    }
}
