﻿/*
 *     Copyright 2017 Adam Burton (adz21c@gmail.com)
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
using Miles.MassTransit.EntityFramework.Implementation.MessageDeduplication;
using System.Data.Entity.ModelConfiguration;

namespace Miles.MassTransit.EntityFramework.Configurations.MessageDeduplication
{
    public class OutgoingMessageConfiguration : EntityTypeConfiguration<OutgoingMessage>
    {
        public OutgoingMessageConfiguration()
        {
            HasKey(x => x.MessageId);
            Property(x => x.ClassTypeName).IsRequired().HasMaxLength(255);

            Property(x => x.SourceAddress).HasMaxLength(255);
            Property(x => x.DestinationAddress).HasMaxLength(255);
            Property(x => x.ResponseAddress).HasMaxLength(255);
            Property(x => x.FaultAddress).HasMaxLength(255);
        }
    }
}
