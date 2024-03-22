using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tetris.Game;
using Tetris.Init;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for _2Play.xaml
    /// </summary>
    public partial class _2Play : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePink.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGray.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileLightPinky.png", UriKind.Relative)),
        };

        // danh sách hình 
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block_Secret.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block_Secret.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block_Secret.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;
        private readonly Image[,] imageControls2;
        private readonly int maxDelay = 300;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 20;


        private int loose1 = 0;
        private int loose2 = 0;

        public int row;
        public int col;
        //public int _w;

        private GameState gameState = new GameState(22, 10);
        private GameState gameState2 = new GameState(22, 10);
        public _2Play()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            GameCanvas.Focus();
            imageControls = SetupGameCanvas(gameState.GameGrid, 1);
            imageControls2 = SetupGameCanvas(gameState2.GameGrid, 2);
        }

        // thêm vị trí các ô vào giao diện 
        private Image[,] SetupGameCanvas(GameGrid grid, int p)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];

            // kích thước ô trên grid
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {

                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,

                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    if (p == 1)
                    {
                        GameCanvas.Children.Add(imageControl);
                    }
                    else if (p == 2)
                    {
                        GameCanvas2.Children.Add(imageControl);

                    }
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        // thêm hình ảnh các ô vào giao diện
        private void DrawGrid(GameGrid grid, int _p)
        {
            try
            {
                for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    if (_p == 1)
                    {
                        imageControls[r, c].Opacity = 1;
                        imageControls[r, c].Source = tileImages[id];
                    }
                    else
                    {
                        imageControls2[r, c].Opacity = 1;
                        imageControls2[r, c].Source = tileImages[id];
                    }
                }
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // vẽ ô vuông 
        private void DrawBlock(Block.Block block, int _p)
        {
            try
            {
                foreach (Position p in block.TilePosition())
                {
                    if (_p == 1)
                    {
                        imageControls[p.Row, p.Column].Opacity = 1;
                        imageControls[p.Row, p.Column].Source = tileImages[block.Id];
                    }
                    else
                    {
                        imageControls2[p.Row, p.Column].Opacity = 1;
                        imageControls2[p.Row, p.Column].Source = tileImages[block.Id];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // vẽ ảnh sẽ rơi xuống ở vị trí nào
        private void DrawGhost(Block.Block block, int _p)
        {
            try
            {
                int dropDistance = gameState.BlockDropDisance();
                if (_p == 1)
                {
                    dropDistance = gameState.BlockDropDisance();
                }
                else
                {
                    dropDistance = gameState2.BlockDropDisance();
                }
                foreach (Position p in block.TilePosition())
                {
                    if (_p == 1)
                    {
                        imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                        imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
                    }
                    else
                    {
                        imageControls2[p.Row + dropDistance, p.Column].Opacity = 0.25;
                        imageControls2[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DrawNextBlock(Block.BlockQueue blockQueue, int _p)
        {
            try
            {
                if (_p == 1) {
                Block.Block next = blockQueue.NextBlock;
                NextImage.Source = blockImages[next.Id];
            } else
            {
                Block.Block next = blockQueue.NextBlock;
                NextImage2.Source = blockImages[next.Id];
            }
        }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
}

        private void Draw(GameState gameState, int _p)
        {
            if (_p == 1)
            {
                DrawGrid(gameState.GameGrid, 1);
                DrawGhost(gameState.CurrentBlock, 1);
                DrawBlock(gameState.CurrentBlock, 1);
                DrawNextBlock(gameState.BlockQueue,1);
                ScoreText.Text = $"Score P1: {gameState.Score}";
            }
            else
            {
                DrawGrid(gameState.GameGrid, 2);
                DrawGhost(gameState.CurrentBlock, 2);
                DrawBlock(gameState.CurrentBlock, 2);
                DrawNextBlock(gameState.BlockQueue,2);
                ScoreText2.Text = $"Score P2: {gameState.Score}";
            }

        }

        private async Task GameLoop()
        {
            Draw(gameState, 1);
            Draw(gameState2, 2);

            while (!gameState.GameOver || !gameState2.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                gameState2.MoveBlockDown();
                Draw(gameState, 1);
                Draw(gameState2, 2);

            }

            if (gameState.GameOver) loose1 += 1;
            if (gameState2.GameOver) loose2 += 1;


            if (gameState.GameOver && gameState2.Score > gameState.Score && loose2> loose1)
            {
                FinalScoreText.Text = $"Score : {gameState2.Score}";
                txtPWin.Text = "P2 Winner";
            }
            else
            {
                FinalScoreText.Text = $"Score : {gameState.Score}";
                txtPWin.Text = "P1 Winner";
            }
            GameOverMenu.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                //case Key.Left:
                //    gameState2.MoveBlockLeft(); break;
                //case Key.Right:
                //    gameState2.MoveBlockRight(); break;
                //case Key.Down:
                //    gameState2.MoveBlockDown(); break;
                //case Key.Up:
                //    gameState2.RotateBlockCW(); break;
                case Key.A:
                    gameState.MoveBlockLeft(); break;
                case Key.D:
                    gameState.MoveBlockRight(); break;
                case Key.S:
                    gameState.MoveBlockDown(); break;
                case Key.W:
                    gameState.RotateBlockCW(); break;

                default:
                    return;
            }

            Draw(gameState, 1);
            //Draw(gameState2, 2);

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (gameState2.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState2.MoveBlockLeft(); break;
                case Key.Right:
                    gameState2.MoveBlockRight(); break;
                case Key.Down:
                    gameState2.MoveBlockDown(); break;
                case Key.Up:
                    gameState2.RotateBlockCW(); break;
                //case Key.A:
                //    gameState.MoveBlockLeft(); break;
                //case Key.D:
                //    gameState.MoveBlockRight(); break;
                //case Key.S:
                //    gameState.MoveBlockDown(); break;
                //case Key.W:
                //    gameState.RotateBlockCW(); break;

                default:
                    return;
            }

            //Draw(gameState, 1);
            Draw(gameState2, 2);

        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState(22, 10);
            gameState2 = new GameState(22, 10);
            GameOverMenu.Visibility = Visibility.Hidden;
             loose1 = 0;
               loose2 = 0;
            await GameLoop();
        }

        private void backToMain(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new Main();
            newWindow.Show();
            Close();
        }

        private void changeOpaMouseMove(object sender, MouseEventArgs e)
        {
            if (back.Opacity == 0.6)
            {
                back.Opacity = 1;
            }
            else if (back.Opacity == 1)
            {
                back.Opacity = 0.6;
            }
        }
    }
}
