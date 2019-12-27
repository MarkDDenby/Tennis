using System;
namespace Tennis
{
    internal class PointsReader
    {
        private readonly string _points;
        private readonly Player _playerA;
        private readonly Player _playerB;
        private int _index;

        public PointsReader(string points, Player playerA, Player playerB)
        {
            if(playerA == null)
            {
                throw new ArgumentNullException("playerA");
            }
            if (playerB == null)
            {
                throw new ArgumentNullException("playerB");
            }
            _points = points;
            _playerA = playerA;
            _playerB = playerB;
        }

        public bool HasPoints()
        {
            return !string.IsNullOrEmpty(_points) && _index < _points.Length;
        }

        public Player ReadPoint()
        {
            char pointForPlayer = _points[_index++];

            if(pointForPlayer == 'A')
            {
                return _playerA;   
            } 
            if (pointForPlayer == 'B')
            {
                return _playerB;
            }
            return null;
        }
    }
}
