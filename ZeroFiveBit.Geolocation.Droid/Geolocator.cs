using System;
using ZeroFiveBit.Geolocation;

namespace ZeroFiveBit.Geolocation.Droid
{
    public class Geolocator : IGeolocator
    {
        public Geolocator()
        {
        }

        #region IGeolocator implementation

        public event EventHandler<PositionErrorEventArgs> PositionError;

        public event EventHandler<PositionEventArgs> PositionChanged;

        public System.Threading.Tasks.Task<Position> GetPositionAsync(int timeout)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Position> GetPositionAsync(int timeout, bool includeHeading)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Position> GetPositionAsync(System.Threading.CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Position> GetPositionAsync(System.Threading.CancellationToken cancelToken, bool includeHeading)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Position> GetPositionAsync(int timeout, System.Threading.CancellationToken cancelToken)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<Position> GetPositionAsync(int timeout, System.Threading.CancellationToken cancelToken, bool includeHeading)
        {
            throw new NotImplementedException();
        }

        public void StartListening(int minTime, double minDistance, bool includeHeading = false, bool always = false)
        {
            throw new NotImplementedException();
        }

        public void StopListening()
        {
            throw new NotImplementedException();
        }

        public double DesiredAccuracy
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsListening
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool SupportsHeading
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsGeolocationAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsGeolocationEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

