using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace PuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {

            DateTime startTime = DateTime.Now;

            int[,] board = new int[5, 11] {
                {1,  1,  1,  9,  9,  9,  9, 11, 11, 11, 11},
                {0,  0,  1,  0,  0,  0,  0,  0,  0, 11,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                {0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0}
            };

            Console.WriteLine("Definierar pusselbitar.");
            int[][,] orgPieces = new int[][,]
            {
                //new int[3, 2] {
                //    {1, 1},
                //    {0, 1},
                //    {0, 1}
                //},

                new int[4, 2] {
                    {2, 0},
                    {2, 2},
                    {0, 2},
                    {0, 2}
                },

                new int[2, 2] {
                    {3, 3},
                    {3, 3}
                },

                new int[3, 3] {
                    {4, 4, 4},
                    {0, 0, 4},
                    {0, 0, 4}
                },

                new int[3, 3] {
                    {0, 5, 0},
                    {5, 5, 5},
                    {0, 5, 0}
                },

                new int[3, 2] {
                    {6, 6},
                    {6, 6},
                    {0, 6}
                },

                new int[3, 2] {
                    {7, 7},
                    {0, 7},
                    {7, 7}
                },

                new int[2, 2] {
                    {8, 8},
                    {0, 8}
                },

                //new int[4, 1] {
                //    {9},
                //    {9},
                //    {9},
                //    {9}
                //},

                new int[4, 2] {
                    {10, 0},
                    {10, 0},
                    {10, 0},
                    {10, 10}
                },

                //new int[4, 2] {
                //    {11, 0},
                //    {11, 0},
                //    {11, 11},
                //    {11, 0}
                //},

                new int[3, 3] {
                    {0, 12, 12},
                    {12, 12, 0},
                    {12, 0, 0}
                }
            };



            //int[,] piece = pieces[3];

            List<List<int[,]>> allPieces = new List<List<int[,]>>();
            //List<List<int[,]>> piece = new List<List<int[,]>>();
            foreach (int[,] orgPiece in orgPieces)
            {
                List<int[,]> alternativePiecePlacements = new List<int[,]>();
                //int[][,] rotPieces = new int[4][,];
                int[,] tempPiece = new int[orgPiece.GetLength(0), orgPiece.GetLength(1)];

                for (int row = 0; row < orgPiece.GetLength(0); row++)
                {
                    for (int col = 0; col < orgPiece.GetLength(1); col++)
                    {
                        tempPiece[row, col] = orgPiece[row, col];
                    }
                }
                if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempPiece)))
                {
                    alternativePiecePlacements.Add(tempPiece.Clone() as int[,]);
                }

                for (int row = 0; row < orgPiece.GetLength(0); row++)
                {
                    for (int col = orgPiece.GetLength(1) - 1; col >= 0; col--)
                    {
                        tempPiece[row, orgPiece.GetLength(1) - 1 - col] = orgPiece[row, col];
                    }
                }
                if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempPiece)))
                {
                    alternativePiecePlacements.Add(tempPiece.Clone() as int[,]);
                }

                for (int row = orgPiece.GetLength(0) - 1; row >= 0; row--)
                {
                    for (int col = 0; col < orgPiece.GetLength(1); col++)
                    {
                        tempPiece[orgPiece.GetLength(0) - 1 - row, col] = orgPiece[row, col];
                    }
                }
                if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempPiece)))
                {
                    alternativePiecePlacements.Add(tempPiece.Clone() as int[,]);
                }

                for (int row = 0; row < orgPiece.GetLength(0); row++)
                {
                    for (int col = orgPiece.GetLength(1) - 1; col >= 0; col--)
                    {
                        tempPiece[orgPiece.GetLength(0) - 1 - row, orgPiece.GetLength(1) - 1 - col] = orgPiece[row, col];
                    }
                }
                if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempPiece)))
                {
                    alternativePiecePlacements.Add(tempPiece.Clone() as int[,]);
                }


                // Kolla om biten kan roteras ytterligare om den inte är fyrkantig.

                if (orgPiece.GetLength(0) != orgPiece.GetLength(1))
                {
                    int[,] invPiece = new int[orgPiece.GetLength(1), orgPiece.GetLength(0)];

                    for (int row = 0; row < orgPiece.GetLength(0); row++)
                    {
                        for (int col = 0; col < orgPiece.GetLength(1); col++)
                        {
                            int val = orgPiece[row, col];
                            invPiece[col, row] = val;
                        }
                    }

                    // Spegelvänt invPiece

                    int[,] tempInvPiece = new int[invPiece.GetLength(0), invPiece.GetLength(1)];

                    for (int row = 0; row < invPiece.GetLength(0); row++)
                    {
                        for (int col = 0; col < invPiece.GetLength(1); col++)
                        {
                            tempInvPiece[row, col] = invPiece[row, col];
                        }
                    }
                    if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempInvPiece)))
                    {
                        alternativePiecePlacements.Add(tempInvPiece.Clone() as int[,]);
                    }

                    for (int row = 0; row < invPiece.GetLength(0); row++)
                    {
                        for (int col = invPiece.GetLength(1) - 1; col >= 0; col--)
                        {
                            tempInvPiece[row, invPiece.GetLength(1) - 1 - col] = invPiece[row, col];
                        }
                    }
                    if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempInvPiece)))
                    {
                        alternativePiecePlacements.Add(tempInvPiece.Clone() as int[,]);
                    }

                    for (int row = invPiece.GetLength(0) - 1; row >= 0; row--)
                    {
                        for (int col = 0; col < invPiece.GetLength(1); col++)
                        {
                            tempInvPiece[invPiece.GetLength(0) - 1 - row, col] = invPiece[row, col];
                        }
                    }
                    if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempInvPiece)))
                    {
                        alternativePiecePlacements.Add(tempInvPiece.Clone() as int[,]);
                    }

                    for (int row = 0; row < invPiece.GetLength(0); row++)
                    {
                        for (int col = invPiece.GetLength(1) - 1; col >= 0; col--)
                        {
                            tempInvPiece[invPiece.GetLength(0) - 1 - row, invPiece.GetLength(1) - 1 - col] = invPiece[row, col];
                        }
                    }
                    if (!alternativePiecePlacements.Exists(element => Is2DArrayEqual(element, tempInvPiece)))
                    {
                        alternativePiecePlacements.Add(tempInvPiece.Clone() as int[,]);
                    }

                }



                // Visa alla sorters lightblue
                //for (int i = 0; i < lightblues.GetLength(2); i++)
                foreach (int[,] rotPiece in alternativePiecePlacements)
                {
                    PrintArray(rotPiece);
                }

                allPieces.Add(alternativePiecePlacements);
            }





            bool fits = false;
            fits = TryFitAllPieces(allPieces, ref board);

            if (fits)
            {
                Console.WriteLine("Final result:");
                PrintArray(board);
            }
            else
            {
                Console.WriteLine("Piece did not fit.");
            }

            TimeSpan endTime = DateTime.Now - startTime;

            //var responseString = new HttpClient().GetStringAsync("https://api.telegram.org/bot/sendMessage?chat_id=123456789&text=Puzzle 480 solved in " + endTime);
            Console.WriteLine("Ended in {0} ({1} - {2})", endTime, startTime, DateTime.Now);
            Console.ReadLine();
        }

        static bool Is2DArrayEqual(int[,] arr1, int[,] arr2)
        {
            bool isEqual = true;
            if (arr1 == null || arr2 == null)
            {
                isEqual = false;
            }
            else if (arr1.Length != arr2.Length)
            {
                isEqual = false;
            }
            else if (arr1.GetLength(0) != arr2.GetLength(0) || arr1.GetLength(1) != arr2.GetLength(1))
            {
                isEqual = false;
            }
            else
            {
                for (int row = 0; row < arr1.GetLength(0); row++)
                {
                    for (int col = 0; col < arr1.GetLength(1); col++)
                    {
                        if (arr1[row, col] != arr2[row, col])
                        {
                            isEqual = false;
                            goto Finish;
                        }
                    }
                }
            }
            Finish:
            return isEqual;
        }


        static void PrintArray(int[,] arr)
        {
            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    //Console.Write(lightblues[row, col, i] + " ");
                    Console.Write(arr[row, col] + (arr[row, col] > 9 ? " " : "  "));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        static bool TryFitAllPieces(List<List<int[,]>> allPieces, ref int[,] board)
        {
            bool fits = false;
            int[,] tempBoard = board.Clone() as int[,];

            for (int i = 0; i < allPieces.Count; i++)
            {
                //progress.Report((double)iterations / calcIterations);
                for (int j = 0; j < allPieces[i].Count; j++)
                {
                    //iterations++;
                    tempBoard = board.Clone() as int[,];
                    //Console.WriteLine("--------------------");
                    //Console.WriteLine("Testar ny vy av pusselbiten.");
                    fits = TryFitPiece(allPieces[i][j], ref tempBoard);
                    if (allPieces.Count > 1)
                    {
                        if (fits)
                        {
                            // Skapa kopia av appPieces
                            List<List<int[,]>> tempAllPieces = new List<List<int[,]>>();
                            foreach (List<int[,]> item in allPieces)
                            {
                                tempAllPieces.Add(item);
                            }

                            tempAllPieces.RemoveAt(i);
                            fits = TryFitAllPieces(tempAllPieces, ref tempBoard);
                            if (fits)
                            {
                                board = tempBoard.Clone() as int[,];
                                return true;
                            }
                        }
                        //else
                        //{
                        //    fits = TryFitAllPieces(allPieces, ref tempBoard);
                        //}
                    }
                    else
                    {
                        if (fits)
                        {
                            board = tempBoard.Clone() as int[,];
                            return true;
                        }
                    }
                }
            }
            if (fits)
            {
                board = tempBoard.Clone() as int[,];
            }
            return fits;
        }

        static bool TryFitPiece(List<int[,]> pieceList, ref int[,] board)
        {
            bool fits = false;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    // För varje plats i board - kolla alla rotationer av biten
                    foreach (int[,] piece in pieceList)
                    {
                        int[,] tempBoard = board.Clone() as int[,];    // Skapa en kopia av board för att kunna ta bort den om biten inte passar.
                        // Gå igenom varje ruta i delen
                        for (int pRow = 0; pRow < piece.GetLength(0); pRow++)
                        {
                            for (int pCol = 0; pCol < piece.GetLength(1); pCol++)
                            {
                                if (piece[pRow, pCol] != 0)
                                {
                                    bool isOutOfRange = false;
                                    bool isOcupied = false;
                                    // Kolla om platsen är utanför board
                                    if (row + pRow >= tempBoard.GetLength(0) || col + pCol >= tempBoard.GetLength(1))
                                    {
                                        isOutOfRange = true;
                                    }
                                    // Kolla om platsen är ledig
                                    else if (tempBoard[row + pRow, col + pCol] != 0)
                                    {
                                        isOcupied = true;
                                    }

                                    if (!isOutOfRange && !isOcupied)
                                    {
                                        tempBoard[row + pRow, col + pCol] = piece[pRow, pCol];
                                        fits = true;
                                    }
                                    else
                                    {
                                        fits = false;
                                        goto AfterFitCheck;
                                    }
                                }
                            }
                        }
                        AfterFitCheck:
                        if (fits)
                        {
                            board = tempBoard.Clone() as int[,];
                            return fits;
                        }
                    }
                }
            }
            return fits;
        }

        static bool TryFitPiece(int[,] piece, ref int[,] board)
        {
            bool fits = false;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    int[,] tempBoard = board.Clone() as int[,];    // Skapa en kopia av board för att kunna ta bort den om biten inte passar.
                    // Gå igenom varje ruta i delen
                    for (int pRow = 0; pRow < piece.GetLength(0); pRow++)
                    {
                        for (int pCol = 0; pCol < piece.GetLength(1); pCol++)
                        {
                            if (piece[pRow, pCol] != 0)
                            {
                                bool isOutOfRange = false;
                                bool isOcupied = false;
                                // Kolla om platsen är utanför board
                                if (row + pRow >= tempBoard.GetLength(0) || col + pCol >= tempBoard.GetLength(1))
                                {
                                    isOutOfRange = true;
                                }
                                // Kolla om platsen är ledig
                                else if (tempBoard[row + pRow, col + pCol] != 0)
                                {
                                    isOcupied = true;
                                }

                                if (!isOutOfRange && !isOcupied)
                                {
                                    tempBoard[row + pRow, col + pCol] = piece[pRow, pCol];
                                    fits = true;
                                }
                                else
                                {
                                    fits = false;
                                    goto AfterFitCheck;
                                }
                            }
                        }
                    }
                    AfterFitCheck:
                    //if (fits)
                    //{
                    //    //Console.WriteLine("Fits = {0}", fits);
                    //    Console.WriteLine("Biten passar!", fits);
                    //    PrintArray(tempBoard);
                    //}
                    if (fits)
                    {
                        board = tempBoard.Clone() as int[,];
                        return fits;
                    }
                }
            }
            return fits;
        }
    }
}
