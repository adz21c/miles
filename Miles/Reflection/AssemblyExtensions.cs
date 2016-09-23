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
using Miles.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Miles.Reflection
{
    public static class AssemblyExtensions
    {
        public static bool IsMessageProcessor(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IMessageProcessor<>);
        }

        public static IEnumerable<Type> GetMessageProcessors(this IEnumerable<Type> types)
        {
            return types.Where(x => x.GetInterfaces().Any(i => i.IsMessageProcessor()));
        }

        public static IEnumerable<Type> GetProcessedMessageTypes(this IEnumerable<Type> types)
        {
            return types.Concat(types.SelectMany(x => x.GetInterfaces()))
                .Where(x => x.IsMessageProcessor()).Select(x => x.GetGenericArguments().First());
        }
    }
}