using System;
using System.Collections.Generic;
using System.Linq;
using ChessGame.Source.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess_Unit_Testing
{
    [TestClass]
    public class UnitTesting
    {
        [TestMethod]
        public void TestTraditionalBoardSetup()
        {
            Board board = new Board();
            Type[] defaultRowPieces =
            {
                typeof(Rook),
                typeof(Knight),
                typeof(Bishop),
                typeof(Queen),
                typeof(King),
                typeof(Bishop),
                typeof(Knight),
                typeof(Rook)
            };
            Type[] createdRowPieces = board.GetRow(0).Select(boardSpace => boardSpace.Piece.GetType()).ToArray();
            if (defaultRowPieces.Length == createdRowPieces.Length)
            {
                for (var i = 0; i < defaultRowPieces.Length; i++)
                {
                    Assert.AreEqual(defaultRowPieces[i], createdRowPieces[i]);
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Test960BoardSetup()
        {
            Board board = new Board(false);
            BoardSpace[] row = board.GetRow(0);
            List<BoardSpace> rowList = row.ToList();
            int kingPos = rowList.IndexOf(rowList.First(p => p.Piece is King));
            int rookOne = rowList.IndexOf(rowList.First(p => p.Piece is Rook));
            int rookTwo = rowList.IndexOf(rowList.Last(p => p.Piece is Rook));
            int bishopOne = rowList.IndexOf(rowList.First(p => p.Piece is Bishop));
            int bishopTwo = rowList.IndexOf(rowList.Last(p => p.Piece is Bishop));
            if (kingPos < rookOne || kingPos > rookTwo || bishopOne % 2 == 0 && bishopTwo % 2 == 0 || bishopOne % 2 == 1 && bishopTwo % 2 == 1)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestGetRow()
        {
            Board board = new Board();
            Assert.IsTrue(board.GetRow(1).All(bs => bs.Piece is Pawn));
        }

        [TestMethod]
        public void TestKillPieceRook()
        {
            Board board = new Board();
            BoardSpace[] row0 = board.GetRow(0);
            board.KillPiece(row0[0]);
            Assert.IsTrue(board.DeadWhites.Last.Value is Rook);
        }
        [TestMethod]
        public void TestKillPieceKing()
        {
            Board board = new Board();
            BoardSpace[] row0 = board.GetRow(0);
            board.KillPiece(row0[0]);
            board.KillPiece(row0[4]);
            Assert.IsTrue(board.DeadWhites.First.Value is King);
        }
    }
}
