//
//  Copyright 2015 Alexey Kinev <rudy@05bit.com>
//
//    Licensed under the Apache License, Version 2.0(the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZeroFiveBit.Geolocation
{
    public interface IGeolocator
    {
        event EventHandler<PositionErrorEventArgs> PositionError;

        event EventHandler<PositionEventArgs> PositionChanged;

        double DesiredAccuracy { get; set; }

        bool IsListening { get; }

        bool SupportsHeading { get; }

        bool IsGeolocationAvailable { get; }

        bool IsGeolocationEnabled { get; }

        Task<Position> GetPositionAsync(int timeout);

        Task<Position> GetPositionAsync(int timeout, bool includeHeading);

        Task<Position> GetPositionAsync(CancellationToken cancelToken);

        Task<Position> GetPositionAsync(CancellationToken cancelToken, bool includeHeading);

        Task<Position> GetPositionAsync(int timeout, CancellationToken cancelToken);

        Task<Position> GetPositionAsync(int timeout, CancellationToken cancelToken, bool includeHeading);

        void StartListening(int minTime, double minDistance, bool includeHeading = false, bool always = false);

        void StopListening();
    }
}

