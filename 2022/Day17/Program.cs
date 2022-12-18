var line = File.ReadLines(@"input.txt").ToList()[0];

var figures = new List<char[,]>
{
    new char[,]
    {
        { '@', '@', '@', '@' },
        { '.', '.', '.', '.' },
        { '.', '.', '.', '.' },
        { '.', '.', '.', '.' },
    },
    new char[,]
    {
        { '.', '@', '.', '.' },
        { '@', '@', '@', '.' },
        { '.', '@', '.', '.' },
        { '.', '.', '.', '.' },
    },
    new char[,]
    {
        { '@', '@', '@', '.' },
        { '.', '.', '@', '.' },
        { '.', '.', '@', '.' },
        { '.', '.', '.', '.' },
    },
    new char[,]
    {
        { '@', '.', '.', '.' },
        { '@', '.', '.', '.' },
        { '@', '.', '.', '.' },
        { '@', '.', '.', '.' },
    },
    new char[,]
    {
        { '@', '@', '.', '.' },
        { '@', '@', '.', '.' },
        { '.', '.', '.', '.' },
        { '.', '.', '.', '.' },
    },
};

var board = new char[9, 40000];
for (int y = 1; y < 40000; y++)
{
    board[0, y] = '|';
    for (int x = 1; x < 8; x++)
        board[x, y] = '.';
    board[8, y] = '|';
}
board[0, 0] = '+';
board[8, 0] = '+';
for (int x = 1; x < 8; x++)
    board[x, 0] = '-';

var top = 0;
var round = 0;
bool first= true;
for (int i = 0; i < 3160; i++)
{
    AddFigure(board, figures[i % 5], top + 4);
    PrintBoard(board);
    bool landed = false;
    var localtop  = top + 6;
    while (!landed)
    {
        if(round % line.Length == 0)
        {
            Console.WriteLine($"{i}: {top}");
        }
        if (line[round % line.Length] == '<')
        {
            MoveLeft(board, localtop);
            PrintBoard(board);
        }
        else
        {
            MoveRight(board, localtop);
            PrintBoard(board);
        }
        (landed, localtop) = moveDown(board, localtop);
        if (landed)
        {
            ChangeToHash(board, localtop);
            //Console.WriteLine(top);
        }
        PrintBoard(board);
        round++;
    }
    if (first)
    {
        top = localtop;
        first = false;
    }
    else
    {
        if (localtop > top)
            top = localtop;
    }
}

Console.WriteLine(top);

void MoveLeft(char[,] board, int top)
{
    for (int y = top+1; y > top - 4 && y > 0; y--)
        for (int x = 7; x > 0; x--)
            if (board[x, y] == '@')
                if (board[x - 1, y] != '@' && board[x - 1, y] != '.')
                    return;

    for (int y = top+1; y > top - 4 && y > 0; y--)
    {
        for (int x = 1; x < 8; x++)
        {
            if (board[x, y] == '@')
            {
                board[x - 1, y] = '@';
                board[x, y] = '.';
            }
        }
    }
}

void MoveRight(char[,] board, int top)
{
    for (int y = top+1; y > top - 6 && y > 0; y--)
        for (int x = 7; x > 0; x--)
            if (board[x, y] == '@')
                if (board[x + 1, y] != '@' && board[x + 1, y] != '.')
                    return;

    for (int y = top+1; y > top - 4 && y > 0; y--)
    {
        for (int x = 8; x > 0; x--)
        {
            if (board[x, y] == '@')
            {
                board[x + 1, y] = '@';
                board[x, y] = '.';
            }
        }
    }
}

(bool, int) moveDown(char[,] board, int top)
{
    int start = top - 6 >= 0 ? top - 6 : 0;
    for (int y = start; y <= top + 1; y++) 
        for (int x = 7; x > 0; x--)
            if (board[x, y] == '@')
                if (board[x, y - 1] != '@' && board[x, y - 1] != '.')
                    return (true, top);

    var newTop = start;
    for (int y = start; y <= top + 1; y++)
    {
        for (int x = 8; x > 0; x--)
        {
            if (board[x, y] == '@')
            {
                if (y - 1 > newTop)
                    newTop = y - 1;
                board[x, y - 1] = '@';
                board[x, y] = '.';
            }
        }
    }
    return (false, newTop);
}

void ChangeToHash(char[,] board, int top)
{
    for (int y = top; y > top - 4 && y > 0; y--)
        for (int x = 7; x > 0; x--)
            if (board[x, y] == '@')
                board[x, y] = '#';
}

void AddFigure(char[,] board, char[,] figure, int top)
{
    for (int y = 0; y < 4; y++)
        for (int x = 0; x < 4; x++)
            board[x + 3, y + top] = figure[y, x];
}

void PrintBoard(char[,] board)
{
    //for (int y = 23; y >= 0; y--)
    //{
    //    for (int x = 0; x < 9; x++)
    //        Console.Write(board[x, y]);

    //    Console.WriteLine();
    //}
    //Console.WriteLine();
}

//TODO refactor and put analysis in code

//lots of manual analysis required
//item: tower height
//1714 : 2685
//3434 : 5387
//5154 : 8089
//6874 : 10791
//8594 : 13493
//10314: 16195
//12034: 18897
//13754: 21599
//15474: 24301
//17194: 27003
//18914: 29705
//
//1714    2685
//3434    5387    1720    2702
//5154    8089    1720    2702
//6874    10791   1720    2702
//8594    13493   1720    2702
//10314   16195   1720    2702
//12034   18897   1720    2702
//13754   21599   1720    2702
//15474   24301   1720    2702
//17194   27003   1720    2702
//18914   29705   1720    2702
//
//581,395,348
//
//1,440
//
//581,395,348 * 2702 =
//1,570,930,230,296
//
//
//1714 + (581, 395, 347 * 1720) = 999,999,998,554
//1000000000000 - 999,999,998,554 = 1,446
//
//1714 + (581, 395, 347 * 1720) + 1,446 = 1000000000000
//
//1,446 + 1714 = 3,160
//
//3,160 + (581, 395, 347 * 2702) = wynik
//4988 + 1,570,930,227,594 = 1,570,930,232,582
//1570930232582