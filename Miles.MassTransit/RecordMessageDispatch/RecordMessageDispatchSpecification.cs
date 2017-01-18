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
using GreenPipes;
using MassTransit;
using Miles.MassTransit.Configuration;
using System.Collections.Generic;

namespace Miles.MassTransit.RecordMessageDispatch
{
    /// <summary>
    /// Configures the <see cref="RecordMessageDispatchFilter{TContext}"/> .
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="global::MassTransit.PipeConfigurators.IPipeSpecification{TContext}" />
    /// <seealso cref="IRecordMessageDispatchConfigurator" />
    class RecordMessageDispatchSpecification<TContext> : IPipeSpecification<TContext>, IRecordMessageDispatchConfigurator
        where TContext : class, SendContext
    {
        public IDispatchedRepository DispatchedRepository { get; private set; }

        public IRecordMessageDispatchConfigurator UseDispatchedRepository(IDispatchedRepository repository)
        {
            this.DispatchedRepository = repository;
            return this;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            if (DispatchedRepository == null)
                yield return new ConfigurationValidationResult(
                    ValidationResultDisposition.Failure,
                    "DispatchedRepository",
                    "Cannot be null",
                    DispatchedRepository.ToString());
        }

        public void Apply(IPipeBuilder<TContext> builder)
        {
            builder.AddFilter(new RecordMessageDispatchFilter<TContext>(DispatchedRepository));
        }
    }
}
