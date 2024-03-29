﻿using Tetris.Init;

namespace Tetris.Block
{
    public class UBlock : Block
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(0, 0),new(0,2), new(1, 0) , new(1, 1), new(1, 2)},
            new Position[] { new(0, 1),new(0,2), new(2, 1) , new(1, 1), new(2, 2) },
            new Position[] { new(2, 0),new(2,2), new(1, 0) , new(1, 1), new(1, 2) },
            new Position[] { new(0, 1), new(0, 0), new(2, 1), new(1, 1), new(2, 0) }
        };

        public override int Id => 9;

        protected override Position[][] Tiles => tiles;

        protected override Position StartOffset => new Position(-1, 3);
    }
}
